<template>
    <section class="bid">
        <p id="bid-id">Bid # {{b.id}}</p>
        <p id="bid-amount">Your bid: ${{ b.amount }}</p>
        <p id="bid-highest">High bid: ${{ item.highestBid }}</p>
        <p id="bid-status">Status:</p>
        <p id="bid-result" v-if="!isActive">Result____</p>
    </section>
</template>

<script>
export default {
    name: "bid",
    props: ['b', 'item'],
    isActive: false, //can we make this computed to reflect unit active status?

}
</script>

<style scoped>
.bid p {
    display: inline;
}


</style>