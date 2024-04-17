<template>
  <div class="container-fluid m-0 p-0">
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
import HomeNavigation from "../components/HomeNavigation.vue"
export default {
  components: {HomeNavigation},
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
  display: grid;
  grid-template-columns: 'header, header, header';

  


  /*background-color: #faefe0;
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
