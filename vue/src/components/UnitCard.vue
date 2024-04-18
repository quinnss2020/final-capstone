<template>
  <v-hover v-slot="{ isHovering, props }">
    <v-card
      :class="{ 'on-hover': isHovering }"
      :elevation="isHovering ? 16 : 2"
      :color="color"
      class="ma-2"
      v-bind="props"
      outlined="transparent"
      id="card"
    >
      <section class="unit-card">
        <div id="writing-box">
          <!-- <div class="pic">
            <img
              id="card-photo"
              :src="firstPhoto.url"
              alt="storage unit for auction"
            />
          </div> -->
          <v-img
            color="surface-variant"
            height="200"
            :src="firstPhoto.url"
            cover
          >
          </v-img>
          <!-- <h3 id="top-line">
            {{ item.city }} #{{ item.id }} | {{ item.size }}
          </h3> -->
          <v-toolbar color="#314668" class="h-10">
              <v-toolbar-title
                class="text-h5 h-50 font-weight-medium"
                color="transparent"
                >
            <div>{{ item.city }} #{{ item.id }}</div>
            </v-toolbar-title>
            </v-toolbar>

          <br />
          <p id="closing-time">
            <Countdown :expiration="item.expiration"></Countdown>
          </p>
          <br />
          <p id="highest-bid">${{ item.highestBid }}</p>
          <br />
          <div class="buttons">
            <v-btn
              elevation="8"
              rounded="xl"
              size="regular"
              :ripple="true"
              v-if="this.$store.state.user.id === 1"
              v-on:click="
                this.$router.push({
                  name: 'adminDetailsView',
                  params: { unitId: item.id },
                })
              "
              >EDIT</v-btn
            >
            <v-btn
              elevation="8"
              rounded="xl"
              size="regular"
              :ripple="true"
              v-else
              v-on:click="
                this.$router.push({
                  name: 'unitDetails',
                  params: { unitId: item.id },
                })
              "
              >BID NOW</v-btn
            >
          </div>
        </div>
      </section>
    </v-card>
  </v-hover>
</template>

<script>
import Countdown from "../components/Countdown.vue";
import PhotoService from "../services/PhotoService.js";

export default {
  name: "unit",
  props: ["item"],
  components: { Countdown },

  data() {
    return {
      expiring: "",
      photos: [],
      firstPhoto: {},
    };
  },

  created() {
    setInterval(this.countdown, 1000);

    PhotoService.getPhotos(this.item.id)
      .then((response) => {
        this.photos = response.data;
        this.firstPhoto = this.photos[0];
      })
      .catch((error) => {
        if (error.response) {
          console.log("Error loading photos: ", error.response.status);
        } else if (error.request) {
          console.log("Error loading photos. Unable to communicate to server.");
        } else {
          console.log("Error making request for unit photos");
        }
      });
  },
};
</script>

<style scoped>
.unit-card {
  display: flex;
  /* border-radius: 2rem; */
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 23vw;
  height: 500px;
  /* margin: 10px; */
}

#card {
  background-color: rgba(244, 236, 225, 0.5);
  border-color: #faefe0;
  border-style: outset;
  border-radius: 2rem;
  height: 470px;
}

#card-photo {
  height: 220px;
  width: 85%;
}

#writing-box {
  width: 100%;
  height: 100%;
}
p {
  font-size: 1.3rem;
  font-weight: 800;
}

h3 {
  font-size: 1.4rem;
}

#closing-time {
  /* background-color: white;
  background-size: 7px 7px; 
   */
  font-size: 20px;
  font-weight: 800;
  color: #d86a46;
  /* background: repeating-linear-gradient(
  0deg,
  transparent,
  transparent 3px,
  #faefe0 3px,
  #314668 50px
), linear-gradient(to bottom ,#faefe0, #314668); */
}

/* .buttons{

} */
</style>
