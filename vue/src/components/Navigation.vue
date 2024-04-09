<template>
    <section class="nav">
        <ol id="nav-buttons">
            <li><a href="#">LISTED UNITS</a></li>
           <li><router-link v-bind:to="{name: 'login'}"> LOGIN </router-link></li>
           <li><a href="#">REGISTER</a></li>
            <!-- <router-link v-bind:to="{name: 'listedUnits'}"> LISTED UNITS </router-link> 
            <router-link v-bind:to="{name: 'login'}"> LOGIN </router-link> 
            <router-link v-bind:to="{name: 'register'}"> REGISTER </router-link> -->
            <li><router-link v-bind:to="{ name: 'logout' }" v-if="this.$store.state.token != ''">LOGOUT</router-link></li>
            </ol>
    </section>
</template>

<script>
</script>

<style scoped>

nav> :is(ul,ol) {
    list-style: none;
   
}
#nav-buttons {
    display: flex;
    flex-flow: row nowrap;
    justify-content: space-around;

    margin: 0;
    list-style: none;
    background: none;
}

a {
    display: flex;
    text-decoration: none;
    background-color: #264B56;
    color: #faefe0;
    font-size: 1.2rem;
    font-weight: 500;
    padding: 2rem;
    border-style: outset;
    border-color: #264B56;
    border-radius: 2rem;
    padding-inline: 1.5rem;
    padding-block: 0.6rem;
}

</style>