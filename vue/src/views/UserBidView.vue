<template>
    <div class="user-bids">
        <h1>These are the user's bids</h1>
        <!--for each bid in the user's bids: create bid div-->
        <section class="bid-container">
            <Bid v-for="bid in userBids" v-bind:key="bid.id" v-bind:b="bid" />
        </section>
        <div class="bid-details">
            <h3>A bid and its details</h3>
        </div>
        <div class="bid-details">
            <h3>A bid and its details</h3>
        </div>
        <div class="bid-details">
            <h3>A bid and its details</h3>
        </div>

    </div>
</template>

<script>
import BidService from '../services/BidService.js';
import Bid from '../components/Bid.vue';

export default {
    name: "UserBids",
    components: { Bid },
    data() {
        return {
            userBids: [],
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
        }

    },

    created() {

        //then call method above that lists bids
        this.getBids();

    }
}

</script>

<style scoped>
.bid-details {
    display: flex;
    border: 2px dashed black;

}
</style>