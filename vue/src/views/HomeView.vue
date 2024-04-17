<template>
  <div class="container-fluid m-0 p-0">
    <div id="home-page" class="top">
      <div class="container pt-3">
        <div class="nav">
          <ol id="nav-buttons">
            <li><router-link v-bind:to="{ name: 'bids' }" v-show="$store.state.token"> MY BIDS </router-link></li>
            <li><router-link v-bind:to="{ name: 'units' }">UNITS</router-link></li>
            <li><router-link v-bind:to="{ name: 'register' }" v-show="!$store.state.token"> REGISTER </router-link></li>
            <li><router-link v-bind:to="{ name: 'logout' }" v-show="$store.state.token">LOGOUT</router-link></li>
            <li><router-link v-bind:to="{ name: 'login' }" v-show="!$store.state.token"> LOGIN </router-link></li>
          </ol>
        </div>
        <div class="row pb-5 pt-4">    
        </div>
      </div>
      <div class="col my-auto">
      <img src="@/assets/DeltaLogo-Light.svg" id="logo" class="float-left img-fluid"/>
      </div>
    </div>

    <div id="bid-win" class="middle">
      <div id="image-unit">
        <img src="../assets/OrangeUnits.svg" id="orange"/>
      </div>
      <div id="text-2">
        <h1>Bid Win Own</h1>
        <p>
          Storage units filled with forgotten treasures
        </p>
        <v-btn elevation="12" rounded="xl" size="x-large">BID NOW</v-btn>
      </div>
    </div>
    
    <div id="expiring" class="bottom">
      <div id="text-3">
        <h1> Expiring Soon </h1>
      </div>
    </div>

    <div id="footer" class="map">
    </div>
  </div>
</template>

<script>
import { stringifyQuery } from 'vue-router';
import image from "../assets/HomePage-Background-Purple.png"
import img from "../assets/DeltaLogo-Light.svg"
import orange from "../assets/OrangeUnits.svg"

export default {
  data: function () {
    return {
      image: image,
      img: img,
      orange : orange
    }
  }
}

</script>

<style scoped>
.home{
  background-image:linear-gradient(#F9F6F0, #AFABA8);
}

.top{
  /* display: flex;
  background-color: #faefe0;
  background-size: cover;
  background-repeat: no-repeat;
  padding-top: 18px;
  padding-bottom: 860px;
  background-repeat: no-repeat;
  justify-content: flex-start;
  flex-direction: column; */
  min-height: 100vh;
}

#home-page
{
  background-image: url("../assets/Background-Home.png");
  background-size: cover;
}

.middle {
  display: flex;
  flex-direction: row;

  min-height: 100vh;

  /* padding-top: 300px;
  display: flex;
  flex-direction: column;
  padding-bottom: 510px;
  align-items: flex-end;
  justify-content: flex-end;
  padding-right: 10px; */
  /* text-align: left; */
}
#image-unit{
  align-self: flex-end;
}
#bid-win{
  background: rgba(244, 236, 225, .5);
}


.bottom{
  min-height: 100vh;
  /* display: flex;
  flex-direction: column;
  width: 100%;
  height: 1000px;

  align-content: flex-start;
  justify-content: flex-start;
  flex-wrap: wrap;
  padding-left: 100px;
  padding-top: 260px; */
 
}

h1{
  color: #314668;
  font-weight: 800;
  font-size: 50px; 
}

p {
  color: #314668;
  font-size: 30px;
  font-weight: 500px;
}

button{
  margin-top: 20px;
  margin-bottom: 20px;
  text-align: center;
}

button:hover {
  color: #9A94BC;
}



nav> :is(ul, ol) {
    list-style: none;

}
.nav {
 display: flex;
 justify-content: flex-end;
}

#nav-buttons {
    display: flex;
    flex-flow: row nowrap;
    margin-top: 10px;
    list-style: none;
    background: none;
    margin-right: 20px;
}


a {
    display: flex;
    text-decoration: none;
    color: #314668;
    font-size: 1.2rem;
    font-weight: 800;
    padding: 2rem;
    border-width: 2px;
    border-style: outset;
    border-color: #9A94BC;
    border-radius: 2rem;
    background-color: #F9F6F0;
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


#logo{
    max-height: 150px;
  
    /* margin-left: 40px; 
    border: none;
    justify-content: flex-start;
    align-items: end;
    padding-top: 600px;
    padding-left: 40px;
    position: absolute;*/
}

#orange{
width: 1200px;

}
</style>
