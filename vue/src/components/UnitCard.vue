<template>
    <section class="unit-card">
        <div id="writing-box">
            <div class="pic">
                <img id="card-photo" :src="firstPhoto.url" alt="storage unit for auction" />
            </div>
            <h3 id="top-line">{{ item.city }} #{{ item.id }} | {{ item.size }}</h3>
            <br>
            <p id="closing-time">Closes in: <Countdown :expiration="item.expiration"></Countdown>
            </p>
            <br>
            <p id="highest-bid">High bid: ${{ item.highestBid }}</p>
            <br>
            <v-btn elevation="8" rounded="xl" size="regular" :ripple="true" v-if="this.$store.state.user.id === 1"
                v-on:click="this.$router.push({ name: 'adminDetailsView', params: { unitId: item.id } })">EDIT</v-btn>
            <v-btn elevation="8" rounded="xl" size="regular" :ripple="true" v-else
                v-on:click="this.$router.push({ name: 'unitDetails', params: { unitId: item.id } })">BID NOW</v-btn>
        </div>
    </section>
</template>

<script>
import Countdown from '../components/Countdown.vue';
import PhotoService from '../services/PhotoService.js';

export default {
    name: 'unit',
    props: ['item'],
    components: { Countdown },

    data() {
        return {
            expiring: "",
            photos: [],
            firstPhoto: {},

        }
    },

    created() {
        setInterval(this.countdown, 1000);


        PhotoService
            .getPhotos(this.item.id)
            .then((response) => {
                this.photos = response.data;
                this.firstPhoto = this.photos[1];
            })
            .catch((error) => {
                if (error.response) {
                    console.log("Error loading photos: ", error.response.status);
                }
                else if (error.request) {
                    console.log("Error loading photos. Unable to communicate to server.");
                }
                else {
                    console.log("Error making request for unit photos");
                }
            })
    }

}


</script>

<style scoped>
.unit-card {

    display: flex;
    background: rgba(235, 229, 220, 0.5);
    border-color: #faefe0;
    border-radius: 2rem;
    border-style: outset;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    width: 300px;
    height: 300px;
    margin: 20px;

}

#card-photo {
    height: 150px;
    width: 200px;
}

#writing-box {

    /* flex-wrap:wrap; */
    /* flex-direction: column;
    justify-content: center;
    align-self: center; */
    width: 250px;
    height: 200px;
}

p {
    font-weight: 700;
}

h3 {
    font-size: 1.2rem;
}

#closing-time {
    font-size: 18px;
    font-weight: 800;
    color: #6a3212a7;
}
</style>