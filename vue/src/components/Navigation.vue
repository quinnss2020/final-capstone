<template>
    <section class="nav">
       <!-- <img :src="image"/> -->
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

import image from "../assets/DeltaStorageSolutionsLogo.svg"
export default {
    data: function() {
        return {
            image : image
        }
    }
}
</script>

<style scoped>

nav> :is(ul,ol) {
    list-style: none;
   
}

#nav-buttons {
    display: flex;
    flex-flow: row nowrap;
    justify-content: flex-end;
    margin-top: 30px;
    list-style: none;
    background: none;
    margin-right: 20px;
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
    margin-right: 20px;
}

img {
    resize: ;
}
</style>