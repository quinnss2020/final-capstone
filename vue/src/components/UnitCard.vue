<template>
    <section class="unit-card" v-on:click="this.$router.push({ name: 'UnitDetailsView', params: { id: unit.id } })">
        <p id="top-line">{{ item.city }} #{{ item.id }} | {{ item.size }}</p>
        <p id="closing-time">Closes in: {{ expiring }} </p>
        <p id="highest-bid">High bid: ${{ item.highestBid }}</p>
        <button>BID NOW</button>


    </section>
</template>

<script>
export default {
    name: 'unit',
    props: ['item'],

    data() {
        return {
          expiring: "",
            
        }
    },

    methods: {
        countdown() {
            var date = new Date(Date.parse(this.item.expiration));
            const now = Date.now();
            let timeRemaining = date - now;
    
            var days = Math.floor(timeRemaining / (1000 * 60 * 60 * 24));
            var hours = Math.floor((timeRemaining % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            var minutes = Math.floor((timeRemaining % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.floor((timeRemaining % (1000 * 60)) / 1000);
            console.log(typeof timeRemaining);
            if (timeRemaining < 0) {
                this.expiring = "EXPIRED"
            }
            else {
            this.expiring = `${days} days ${hours} hours ${minutes} minutes ${seconds} seconds `;
            }
        },
    },
    
    computed: {
        

    },

    created() {
        setInterval(this.countdown, 1000);

    }


}

</script>

<style scoped>
.unit-card {
    border: 2px solid white;
    display: grid;
    /* flex-direction: column; */
    /* width: 10vw;
    height: 400px; */

}
</style>