<template>
    <section class="unit-card" v-on:click="this.$router.push({name: 'UnitDetailsView', params: {id: unit.id}})">
        <p id="top-line">City: {{ item.location }} #{{ item.id }} | Size: {{ item.size }}</p>
        <p id="closing-time">Closes in: {{ item.expiration }}</p>
        <p id="highest-bid"></p>
        <button></button>

        
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