<template>
    <div class="user-bids">
        <h1>My Bid History</h1>
        <!--for each bid in the user's bids: create bid div-->
        <section class="bid-container">
            <table id="bid-table">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Auction</th>
                        <th>Current Status</th>
                        <th>Time Remaining</th>
                        <th>Bid Amount </th>
                        <th>High Bid</th>
                        <th>Outcome</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="{bid, unit} in bidsAndUnits" v-bind:key="bid.amount">
                        <td>{{ bid.id }}</td>
                        <td id="unit-link">
                            <button v-on:click="this.$router.push({ name: 'unitDetails', params: { unitId: unit.id }})">{{unit.city}} #{{unit.id}}</button>
                        </td>
                        <td>{{ currentStatus(unit) }}</td>
                        <td id="unit-time-remaining">
                            <Countdown :expiration="unit.expiration"></Countdown>
                        </td>
                        
                        <td>${{ bid.amount }}</td>
                        <td>${{unit.highestBid}}</td>
                        <td>{{outcome(unit, bid)}}</td>

                    </tr>
                    
                </tbody>

            </table>


            <!-- <Bid v-for="bid in userBids" v-bind:key="bid.amount" v-bind:b="bid" /> -->
        </section>

    </div>
</template>

<script>
import BidService from '../services/BidService.js';
import Bid from '../components/Bid.vue';
import UnitCard from '../components/UnitCard.vue';
import UnitService from '../services/UnitService.js';
import Countdown from '../components/Countdown.vue';

export default {
    name: "UserBids",
    //props: ['item'],
    components: { Countdown },
    data() {
        return {
            userBids: [],
            //thisUnit: {},
            units: [],
        }
    },

    methods: {
        // countdown(expiration) {
        //     return countdown(expiration);
        // },
        getBids() {
            BidService
                .list()
                .then((response) => {
                    this.userBids = response.data;
                })
                .catch((error) => {
                    if (error.response) {
                        console.log("Error loading bids: ", error.response.status);
                    }
                    else if (error.request) {
                        console.log("Error loading bids. Unable to communicate to server.");
                    }
                    else {
                        console.log("Error making request");
                    }
                })
        },

        getUnitDetails(unitId) {

            return this.units.find((u) => u.id === unitId);
            // UnitService
            //     .unitDetails(this.bid.unitId)
            //     .then((response) => {
            //         this.thisUnit = response.data;
            //     })
            //     .catch((error) => {
            //         if (error.response) {
            //             console.log("Error loading unit details: ", error.response.status);
            //         }
            //         else if (error.request) {
            //             console.log("Error loading unit details. Unable to communicate to server.");
            //         }
            //         else {
            //             console.log("Error making request to fetch unit details");
            //         }
            //     })
            

        },

        getUnits(){
            UnitService
                .list()
                .then((response) => {
                    this.units = response.data;
                })
                .catch((error) => {
                    if (error.response) {
                        console.log("Error loading unit details: ", error.response.status);
                    }
                    else if (error.request) {
                        console.log("Error loading unit details. Unable to communicate to server.");
                    }
                    else {
                        console.log("Error making request to fetch unit details");
                    }
                })
        },

        currentStatus(unit) {
            if(unit.active) {
                return "ACTIVE";
            }
            else {
                return "INACTIVE";
            }
        },

        outcome(unit, bid) {
            if(unit.highestBidder === bid.bidderId && unit.active === false) {
              return "WON";
            }
            else if(unit.active === false) {
                return "LOST";
            }
            else {
                return "";
            }
        }


    },

    computed: {
        bidsAndUnits() {
            //.map function
            return this.userBids.map( (bid) => {
                return {
                    bid,
                    unit: this.getUnitDetails(bid.unitId)
                }
            })
            
        },


    },



    created() {

        //then call method above that lists bids
        this.getBids();
        this.getUnits();
        //this.getUnitDetails();
        

    }
}

</script>

<style scoped>
.bid-details {
    display: flex;

}

.bid-container {
    display: flex;
    justify-content: center;
}

h1 {
    color: #314668;
}

table {
    margin: 10px;
    border-collapse: separate;
    /*border-spacing: 10px; */
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
    padding: 2rem;

}
</style>