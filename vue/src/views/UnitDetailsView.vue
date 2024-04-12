<template>
    <div class="unit-details">
        <h1>This is the Unit Details View</h1>
        <div id="container">
            <div id="writing-box">
                <h3>{{ unit.city }} Unit #{{ unit.id }}</h3>
                <h3>Highest Bid: ${{ unit.highestBid }}</h3>
                <form v-on:submit.prevent="placeBid()">
                    <input type="text" placeholder="Enter Bid Amount" id="bid-amount" name="bid-amount" v-bind="bid.amount">
                    <br>
                    <br>
                    <button v-on:click="this.$router.push('/bids')">BID NOW</button>
                </form>
                <h3>Details:</h3>
            </div>
            <div id="images-box">
                {Pictures Here of Unit}
            </div>
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
                id: this.unitId,
                bidderId: this.$store.state.user.id,
                amount: "",
                dateTime: "",
            },
            unitId: 0
        }
    },

    methods: {
        placeBid(){
            BidService
            .bid(this.bid)
            .then((response) =>{
            if (response.status === 201){
                this.$router.push({
                    path: '/bids'
                });
            }
            })
            .catch((error) => {
                const response = error.response;
                if (response.status === 400)
                {
                    console.log("Error placing bid.")
                }
            })
         
        }    
    },

    created() {
        this.unitId = this.$route.params.id
        UnitService
            .unitDetails(this.unitId)
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

<style scoped></style>