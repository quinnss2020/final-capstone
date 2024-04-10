<template>
    <section class="unit-card" v-on:click="this.$router.push({name: 'UnitDetailsView', params: {id: unit.id}})">
        <p id="top-line">{{ item.city }} #{{ item.id }} | {{ item.size }}</p>
        <p id="closing-time">Closes in: {{ item.expiration }}</p>
        <p id="highest-bid">High bid: ${{ item.highestBid }}</p>
        <button>BID NOW</button>

        
    </section>
</template>

<script>
export default {
    name: 'unit',
    props: ['item'],
    computed: {
        //computed time for when bid closes
    },
}

</script>

<style scoped></style>