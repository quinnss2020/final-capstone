<template>
    <section class="unit-card">
        <div id="writing-box">
        <p id="top-line">{{ item.city }} #{{ item.id }} | {{ item.size }}</p>
        <p id="closing-time">Closes in: <Countdown :expiration="item.expiration"></Countdown></p>
        <p id="highest-bid">High bid: ${{ item.highestBid }}</p>
    </div>
    <button v-if="this.$store.state.user.id === 1" v-on:click="this.$router.push({ name: 'adminDetailsView', params: { unitId: item.id }})">EDIT</button>
    <button v-else v-on:click="this.$router.push({ name: 'unitDetails', params: { unitId: item.id }})">BID NOW</button>
    </section>
</template>

<script>
import Countdown from '../components/Countdown.vue';

export default {
    name: 'unit',
    props: ['item'],
    components: { Countdown },

    data() {
        return {
          expiring: "",
            
        }
    },

    methods: {

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
   
    display: flex;
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

#writing-box{
    display: flex; 
    flex-wrap:wrap;
    flex-direction: column;
    justify-content: center;
    align-self: center;
    width: 250px;
    height: 200px;
}
</style>