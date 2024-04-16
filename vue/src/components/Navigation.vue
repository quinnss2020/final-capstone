<template>
    <section class="nav">
        <ol id="nav-buttons">
            <router-link id="logo" v-bind:to="{ name: 'home' }"><img src="../assets/DeltaLogoDark.svg" id="logo"/></router-link>
            <li> <router-link v-bind:to="{ name: 'bids' }" v-show="$store.state.token"> MY BIDS </router-link></li>
            <li><router-link v-bind:to="{ name: 'units' }">UNITS</router-link></li>
            <li> <router-link v-bind:to="{ name: 'register' }" v-show="!$store.state.token"> REGISTER </router-link></li>
            <li><router-link v-bind:to="{ name: 'logout' }" v-show="$store.state.token">LOGOUT</router-link></li>
            <li><router-link v-bind:to="{ name: 'login' }" v-show="!$store.state.token"> LOGIN </router-link></li>
        </ol>
    </section>
</template>

<script>

import image from "../assets/DeltaLogoDark.svg"
export default {
    data: function () {
        return {
            image: image
        }
    }
}
</script>

<style scoped>
nav> :is(ul, ol) {
    list-style: none;

}

#nav-buttons {
    display: flex;
    flex-flow: row nowrap;
    justify-content: flex-end;
    margin-top: 35px;
    list-style: none;
    background: none;
    margin-right: 20px;
}


a {
    display: flex;
    text-decoration: none;
    /* background-color: #264B56; */
    color: #314668;
    font-size: 1.2rem;
    font-weight: 800;
    padding: 2rem;
    border-width: 2px;
    border-style: outset;
    border-color: #9A94BC;
    border-radius: 2rem;
    padding-inline: 1.5rem;
    padding-block: 0.6rem;
    margin-right: 20px;
}

a:hover {
    /* background-image: linear-gradient(to right, #F9F6F0, #8FB8DE); */
    animation-name: example;
    animation-duration: 1s;
}

@keyframes example {
    from {background-color:#F9F6F0;}
    to {background-color:  #d5d3e3;}  /*#d5d3e3*/
    
}

ol>#logo {
    height: 70px;
    margin-right: auto;
    margin-left: 30px;
    border: none;
}
</style>