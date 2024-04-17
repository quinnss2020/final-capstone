<template>
  
  <div id="capstone-app">
    <navigation v-if="!$route.meta.hideNavigation"/> <!--v-show = "!this.$route.path === 'home'"-->
    <!-- <div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'logout' }" v-if="this.$store.state.token != ''">Logout</router-link>
    </div> -->
    <router-view />
    <Footer />
  </div>
</template>

<script>
import Navigation from "./components/Navigation.vue";
import Footer from './components/Footer.vue';

export default {
  name: 'App',
  components: { Navigation, Footer },

}

</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap');
@import url('https://fonts.googleapis.com/css2?family=Urbanist:ital,wght@0,100..900;1,100..900&display=swap');
@import url('https://fonts.googleapis.com/css2?family=Nunito+Sans:ital,opsz,wght@0,6..12,200..1000;1,6..12,200..1000&family=Open+Sans:ital,wght@0,300..800;1,300..800&family=Urbanist:ital,wght@0,100..900;1,100..900&display=swap');

html, body {
    min-height: 100%;
}


html {
  font-family: "Nunito Sans", sans-serif !important;
  text-align: center;
  color: #314668;
  /* background-color:  #F9F6F0; */
  background-image: linear-gradient(#F9F6F0, #8393C2);
  background-attachment: fixed;
  margin: 0;
  height: 100vh;
  width: 100vw;
  padding: 0;
  top: 0;
  bottom: 0;
}

button {
  font-family: "Nunito Sans", sans-serif !important;
  font-weight: 730;
  color: #314668;
  border-style: outset;
  border-color: #F4ECE1;
  border-radius: 0.6rem;
  padding: 8px;
  width: 170px;
  height: 35px;
  margin-bottom: 7px;
  white-space: nowrap;
  /* background-color: #faefe0; */
  /* background-color:#9A94BC; */
}

input {
  background-color: #F9F6F0;
  border: none;
  border-radius: 0.3rem;
  padding: 8px;
}

/* .slide-enter-from,
.slide-leave-to {
opacity: 0;
transform: translateX(-100%);
}

.slide-enter-active,
.slide-leave-active {
  transition: 0.3s ease-out;
} */
</style>
