<template>
    <div class="unit-details">
        <div id="details-container">
            <div id="images-box">
                <h1>- Pictures Here -</h1>
            </div>
            <div id="writing-box">
                <h2>{{ unit.city }} Unit #{{ unit.id }}</h2>
                <h3 class="emphasis"><Countdown :expiration="unit.expiration"></Countdown></h3>
                <h3 class="highest-bid">Highest Bid: ${{ unit.highestBid }}</h3>
                <p class="bid-error-msg" v-if="bidErrors">{{ this.bidErrorMsg }}</p>
                
                <form v-on:submit.prevent="placeBid()">
                    <input type="text" placeholder="Enter Bid Amount" id="bid-amount" name="bid-amount"
                        v-model.number="bid.amount">
                    <br>
                    <br>
                    <button type="submit">BID NOW</button>
                </form>
                <h3>Details:</h3>
                <h3 id="details-text">{{ unit.details }}</h3>
            </div>
        </div>
    <div id="bid-history-container" v-if="this.$store.state.user.id === 1">
        <br><br>
            <h2 id="history-title">Bid History</h2>
            <table id="bid-history">
                <thead>
                    <tr>
                        <th>Bid ID</th>
                        <th>Unit ID</th>
                        <th>Bidder ID</th>
                        <th>Amount</th>
                        <th>Date Placed</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="bid in bidsByUnit" v-bind:key="bid.amount">
                        <td>{{ bid.id }}</td>
                        <td>{{ bid.unitId }}</td>
                        <td>{{ bid.bidderId }}</td>
                        <td>${{ bid.amount }}</td>
                        <td>{{ bid.datePlaced }}</td>
                    </tr>
                </tbody>
                 </table>

    </div>
    </div>
</template>

<script>
import UnitService from '../services/UnitService';
import BidService from '../services/BidService';
import Countdown from '../components/Countdown.vue';

export default {
    name: "UnitDetails",
    components: {Countdown},
    data() {
        return {
            unit: {},
            bidsByUnit: [],

            bid: {
                unitId: "",
                bidderId: this.$store.state.user.id,
                amount: "",

            },
            bidErrors: false,
            bidErrorMsg: 'Bid must be higher than current bid.',

        }
    },

    methods: {
        placeBid() {
            
            if (this.bid.amount <= this.unit.highestBid) {
                this.bidErrors = true;

            }
            else if (!Number.isInteger(this.bid.amount)) {
                this.bidErrors = true;
                this.bidErrorMsg = 'Bid must be a whole number. Do not use decimals';
            }
            else if (!this.$store.state.token) {
                this.$router.push("/login");
            }
            else {
                BidService
                    .bid(this.bid)
                    .then((response) => {
                        if (response.status == 201) {
                            this.$router.push({
                                path: '/bids'
                            });
                        }
                    })
                    .catch((error) => {
                        const response = error.response;
                        if (response.status === 400) {
                            console.log("Error placing bid.")
                        }
                    })
            }
        },

        getBidsByUnit() {
            BidService
            .bidHistory(this.bid.unitId)
            .then((response) => {
                this.bidsByUnit = response.data;
            })
            .catch((error) => {
                if (error.response) {
                    console.log("Error loading bid history: ", error.response.status);
                }
                else if (error.request) {
                    console.log("Error loading bid history. Unable to communicate to server.");
                }
                else {
                    console.log("Error making request for bid history");
                }
            })
        }
    },

    created() {
        
        this.bid.unitId = this.$route.params.unitId;
        this.getBidsByUnit();
        UnitService
            .unitDetails(this.bid.unitId)
            .then((response) => {
                this.unit = response.data;
            })
            .catch((error) => {
                if (error.response) {
                    console.log("Error loading unit: ", error.response.status);
                }
                else if (error.request) {
                    console.log("Error loading unit. Unable to communicate to server.");
                }
                else {
                    console.log("Error making request");
                }
            });

            
    }
}

</script>

<style scoped>

h2 {
    font-size: 3rem;
    margin: 0;
}


h3 {
    text-align: left;
    margin: 0;
}

form {
    margin-top: 20px;
}

button {
    margin-bottom: 30px;
}

h3.emphasis {
    color: #FF7243;
}

#details-text{
    font-weight: 300;
}

.highest-bid {
    margin:0;
    font-size: 2rem;
}

#details-container {
    display: grid;
    grid-template-columns: 1fr 1fr;
    height: 500px;
    flex-wrap: nowrap;
    justify-content: center;
    align-items: center;
    margin-right: 20px;
    margin-left: 20px;

}

#writing-box {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    border-style: outset;
    border-color: #F9F6F0;
    border-radius: 2rem;
    padding: 30px;
    margin-left: 0;
    background: rgba(244, 236, 225, .5);
    color: #314668;
}

#images-box {
    border-color: #faefe0;
    border-radius: 2rem;
    border-style: outset;
    /* height: 300px; */
    margin-right: 80px;
    /* padding: 20px; */
}

.bid-error-msg {
    color: #FF7243;
    font-weight: bold;
}

#bid-history-container {
    display: flex;
    flex-direction: column;
    justify-content: center;
    background-color: rgba(239, 237, 255, 0.8);
}

#bid-amount {
    width: 155px;
}

table {
    margin: 10px;
    border-collapse: separate;
}

tbody tr:nth-child(odd) {
    background-color: #8393C2;
    padding: 2rem;

}

tbody tr:nth-child(even) {
    background: rgba(244, 236, 225, .2);

}

td {
    padding: 30px;
}

th {
    color: #314668;
    font-size: 1.5rem;
}
</style>