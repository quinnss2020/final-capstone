<template>
    <section class="bid">
        <p id="bid-id">Bid # {{b.id}}</p>
        <p id="bid-amount">Your bid: ${{ b.amount }}</p>
        <p id="bid-highest">High bid: $</p>
        <p id="bid-status">Status:</p>
        <p id="bid-result" v-if="!isActive">Result____</p>
    </section>
</template>

<script>
export default {
    name: "Bid",
    props: ['b'],
    isActive: false, //can we make this computed to reflect unit active status?

}
</script>

<style scoped>
.bid p {
    display: inline;
    border: 1px solid white;
    padding: 10px;
    margin-bottom: 30px;
}

</style>