<template>
    <div class="unit-details">
        <h1>This is the Unit Details View</h1>
        <div id="container-details">
            <div id="images-box">
                <h1>- Pictures Here -</h1>
            </div>
            <div id="writing-box">
                <h2>{{ unit.city }} Unit #{{ unit.id }}</h2>
                <h3>Highest Bid: ${{ unit.highestBid }}</h3>
                <p class="bid-error-msg" v-if="bidErrors">{{ this.bidErrorMsg }}</p>
                
                <form v-on:submit.prevent="placeBid()">
                    <input type="text" placeholder="Enter Bid Amount" id="bid-amount" name="bid-amount"
                        v-model.number="bid.amount">
                    <br>
                    <br>
                    <button type="submit">BID NOW</button>
                </form>
                <h3>Details:</h3>
            </div>
        </div>
        <div id="bid-history-container">
            <h1>Bid History</h1>

        </div>
    </div>
</template>

<script>
import UnitService from '../services/UnitService'
import BidService from '../services/BidService'

export default {

    data() {
        return {
            unit: {},

            bid: {
                unitId: "",
                bidderId: this.$store.state.user.id,
                amount: "",

            },
            bidErrors: false,
            bidErrorMsg: 'Bid must be higher than current bid. Enter whole number.',

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


        }
    },

    created() {
        //supposively
        this.bid.unitId = this.$route.params.unitId;
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
            })
    }
}

</script>

<style scoped>
h1 {
    padding: 20px;
}

#container-details {
    display: flex;
    flex-direction: row;
    height: 500px;
    flex-wrap: nowrap;
    justify-content: center;
    align-items: center;
    background-color: rgb(48, 66, 78);
}

#writing-box {
    border-style: outset;
    border-color: #faefe0;
    border-radius: 2rem;
    margin-left: 100px;
    padding: 50px;
    background-color: #B4B09B;
    color: #264B56;
}

#images-box {
    border-color: #faefe0;
    border-radius: 2rem;
    border-style: outset;
    height: 300px;
    margin-right: 100px;
    padding: 20px;
}

#bid-history-container {
    background-color: #B4B09B;
    height: 200px;
}

.bid-error-msg {
    color: red;
    font-weight: bold;
}
</style>