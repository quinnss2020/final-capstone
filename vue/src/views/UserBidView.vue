<template>
    <div class="user-bids">
        <h1>These are the user's bids</h1>
        <!--for each bid in the user's bids: create bid div-->
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

export default {

    methods: {
        listBids(){
            //list all bids
            return null;
        }

    },

    created() {
        BidService
        //TODO ensure that server controller is taking user's ID and only returning their bids
        .list()

        //then call method above that lists bids
        this.listBids();

    }
}

</script>

<style scoped>
.bid-details {
    display: flex;
    border: 2px dashed black;

}
</style>