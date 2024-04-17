<template>
  <div class="home">
    <div id="home-page" class="top">
      <div class="nav">
        <HomeNavigation/>
      </div>
      <div class="delta-logo">
      <img src="@/assets/DeltaLogo-Light.svg" id="logo" />  <!-- class="float-left" -->
      </div>
    </div>

    <div id="bid-win" class="middle">
      <div id="image-unit">
        <img src="../assets/OrangeUnits.svg" id="orange">
      </div>
      <div id="text-2">
        <h1>Bid Win Own</h1>
        <p>
          Storage units filled with forgotten treasures
        </p>
        <br>
        <v-btn elevation="12" rounded="xl" size="x-large">BID NOW</v-btn>
      </div>
    </div>
    
    <div id="expiring" class="bottom">
      <div id="text-3">
        <h1> Expiring Soon </h1>
  
      </div>
      
      <div id="unit-cards">
        <UnitCard v-for="unit in getTopThree" v-bind:key="unit.id" v-bind:item="unit"></UnitCard>
      </div>
    </div>

  </div>
</template>

<script>
import { stringifyQuery } from 'vue-router';
import image from "../assets/HomePage-Background-Purple.png"
import img from "../assets/DeltaLogo-Light.svg"
import orange from "../assets/OrangeUnits.svg"
import HomeNavigation from "../components/HomeNavigation.vue"
import UnitCard from '../components/UnitCard.vue';
import UnitService from '../services/UnitService';


export default {
  components: {HomeNavigation, UnitCard},

  data: function () {
    return {
      units: [],
      image: image,
      img: img,
      orange : orange
    }
  },

  computed: {
    getTopThree() {
     return this.units.slice(0,3);
    }
  },

  methods: {
    getUnits() {
            UnitService
                .list()
                .then((response) => {
                    this.units = response.data;
                })
                .catch((error) => {
                    if (error.response) {
                        console.log("Error loading units: ", error.response.status);
                    }
                    else if (error.request) {
                        console.log("Error loading units. Unable to communicate to server.");
                    }
                    else {
                        console.log("Error making request");
                    }
                })
        },
  },

  created() {
   this.getUnits();
  }

}


</script>

<style scoped>
.home{
  background-image:linear-gradient(#F9F6F0, #AFABA8);
}

.top{
  display: grid;
  min-height: 100vh;
  background-image: url("../assets/Background-Home.png");
  background-size: cover;
}
.delta-logo{
  display: flex;
  justify-content: center;
  align-items: center;
  background-image: linear-gradient(to right, rgba(175, 171, 168, 0.7), rgba(49, 70, 104, 3));
  height: 170px;
}
#logo{
  max-height: 170px;
  
}
.middle {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  padding-right: 10px; 
  padding-top: 10px;
  text-align: left; 
  height: 80vh;
}

#image-unit{
  padding-top: 2px;
  padding-right: 90px;
}
#orange{
  width: 800px;
  box-shadow: 20px 20px rgba(46, 39, 82, 0.5);
}

.bottom{
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  background-image: linear-gradient(#AFABA8, #F9F6F0);
/*flex-direction: column;
  width: 100%;
  height: 1000px;
  align-content: flex-start;
  justify-content: flex-start;
  flex-wrap: wrap;
  padding-left: 100px;
  padding-top: 260px; */
 
}

#unit-cards{
display: flex;
align-self: center;
}

section.unit-card{
margin-right: 40px;
margin-top: 40px;
}

#text-3{
  align-self: flex-start;
  justify-self: start;
  padding-left: 100px;
  padding-top: 60px; 

}

h1{
  color: #314668;
  font-weight: 800;
  font-size: 80px; 
}

p {
  color: #314668;
  font-size: 30px;
  font-weight: 500px;
}

button{
  margin-top: 40px;
  margin-bottom: 20px;
  text-align: center;
}

button:hover {
  color: #9A94BC;
}

</style>
