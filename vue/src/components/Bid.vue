<template>
    <section class="bid">
        <p id="bid-id">Bid # ___</p>
        <p id="bid-amount">Your bid: $____</p>
        <p id="bid-highest">High bid: $____</p>
        <p id="bid-status">Status: ____</p>
        <p id="bid-result" v-if="!isActive">Result____</p>
    </section>
</template>

<script>
export default {
    name: "bid",
    props: ['b'],
    isActive: false, //can we make this computed to reflect unit active status?

}
</script>

<style scoped>
.bid p {
    display: inline;
}


</style>