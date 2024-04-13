<template>
    <div class="user-bids">
        <h1>My Bid History</h1>
        <!--for each bid in the user's bids: create bid div-->
        <section class="bid-container">
            <table id="bid-table">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Auction [LINK TO UNIT PAGE]</th>
                        <th>Current Status</th>
                        <th>Time Remaining</th>
                        <th>Bid Amount </th>
                        <th>Current High Bid</th>
                        <th>Outcome [COMPUTED WON OR LOST]</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="bid in userBids" v-bind:key="bid.amount" v-bind:b="bid">
                        <td>{{ bid.id }}</td>
                        <td>the auction link</td>
                        <td>COMPUTED</td>
                        <td id="unit-time-remaining">text here</td>
                        <td>${{ bid.amount }}</td>
                        <td>text</td>
                        <td>COMPUTED</td>

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

export default {
    name: "UserBids",
    //props: ['item'],
    // components: { UnitCard },
    data() {
        return {
            userBids: [],
            thisUnit: {},
        }
    },

    methods: {
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

        getUnitDetails() {
            UnitService
                .unitDetails(this.bid.unitId)
                .then((response) => {
                    this.thisUnit = response.data;
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

    },

    created() {

        //then call method above that lists bids
        this.getBids();
        //this.getUnitDetails();
        

    }
}

</script>

<style scoped>
.bid-details {
    display: flex;
    border: 2px dashed black;

}

.bid-container {
    display: flex;
    justify-content: center;
}

h1 {
    color: #faefe0;
}

table {
    margin: 10px;
}

td {
    padding: 50px;
}

th {
    color: #faefe0;
    font-size: 1.5rem;
}
</style>