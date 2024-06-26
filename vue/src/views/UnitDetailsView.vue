<template>
  <div class="unit-details">
    <div id="details-container">
      <div id="images-box">
        <v-container v-if="this.loading">
          <v-carousel id="vc" show-arrows="hover" v-model="slider">
            <v-carousel-item id="image" v-for="(photo, id) in photos" :key="id" eager>
                <v-img class="v-img" :class="hover ? 'zoom' : ''" :src="photo.url" eager />
            </v-carousel-item>
          </v-carousel>
        </v-container>
      </div>
      <div id="writing-box">
        <h2>{{ unit.city }} Unit #{{ unit.id }}</h2>
        <h3 class="emphasis">
          <Countdown :expiration="unit.expiration"></Countdown>
        </h3>
        <h3 class="highest-bid">Highest Bid: ${{ unit.highestBid }}</h3>
        <p class="bid-error-msg" v-if="bidErrors">{{ this.bidErrorMsg }}</p>
        <form v-on:submit.prevent="placeBid()">
          <input type="text" placeholder="Enter Bid Amount" id="bid-amount" name="bid-amount"
            v-model.number="bid.amount" />
          <br />
          <br />
          <v-btn type="submit">BID NOW</v-btn>
        </form>
        <br>
        <h3>Details:</h3>
        <h3 id="details-text">{{ unit.details }}</h3>
      </div>
    </div>
  </div>
</template>

<script>
import UnitService from "../services/UnitService";
import BidService from "../services/BidService";
import Countdown from "../components/Countdown.vue";
import PhotoService from "../services/PhotoService";

export default {
  name: "UnitDetails",
  components: { Countdown },
  data() {
    return {
      unit: {},
      bidsByUnit: [],
      slider: 0,
      bid: {
        unitId: "",
        bidderId: this.$store.state.user.id,
        amount: "",
      },
      bidErrors: false,
      bidErrorMsg: "Bid must be higher than current bid.",
      photos: [],
      loading: false,
    };
  },

  methods: {
    placeBid() {
      if (this.bid.amount <= this.unit.highestBid) {
        this.bidErrors = true;
      } else if (!Number.isInteger(this.bid.amount)) {
        this.bidErrors = true;
        this.bidErrorMsg = "Bid must be a whole number. Do not use decimals";
      } else if (!this.$store.state.token) {
        this.$router.push("/login");
      } else {
        BidService.bid(this.bid)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: "/bids",
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            if (response.status === 400) {
              console.log("Error placing bid.");
            }
          });
      }
    },

    getBidsByUnit() {
      BidService.bidHistory(this.bid.unitId)
        .then((response) => {
          this.bidsByUnit = response.data;
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error loading bid history: ", error.response.status);
          } else if (error.request) {
            console.log(
              "Error loading bid history. Unable to communicate to server."
            );
          } else {
            console.log("Error making request for bid history");
          }
        });
    },

    getPhotoList() {
      PhotoService
        .getPhotos(this.unit.id)
        .then((response) => {
          this.photos = response.data;
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error loading photos: ", error.response.status);
          } else if (error.request) {
            console.log(
              "Error loading photos. Unable to communicate to server."
            );
          } else {
            console.log("Error making request");
          }
        });
    },

  },

  created() {
    this.bid.unitId = this.$route.params.unitId;
    this.getBidsByUnit();
    UnitService.unitDetails(this.bid.unitId)
      .then((response) => {
        this.unit = response.data;
        console.log("This is not a dance")
        this.getPhotoList();
    this.loading = true;
      })
      .catch((error) => {
        if (error.response) {
          console.log("Error loading unit: ", error.response.status);
        } else if (error.request) {
          console.log("Error loading unit. Unable to communicate to server.");
        } else {
          console.log("Error making request");
        }
      });
  },

  mounted() {
    //document.getElementById('vc').firstChild.style.display = "inherit";
    //this.$el.querySelector('v-carousel__controls'.firstChild.style.display = 'flex')
  }
};
</script>

<style scoped>
h2 {
  font-size: 3rem;
  margin: 0;
}

h3 {
  text-align: left;
  margin: 0;
}

form {
  margin-top: 20px;
}

button {
  margin-bottom: 30px;
}

h3.emphasis {
  font-size: x-large;
  color: #ff7243;
}

#details-text {
  font-weight: 300;
}

.highest-bid {
  margin: 0;
  font-size: 2rem;
  padding-bottom: 5px;
}

#details-container {
  display: grid;
  grid-template-columns: 1fr 1fr;
  flex-wrap: nowrap;
  justify-content: center;
  align-items: center;
  height: 500px;
  margin-top: 30px;
  margin-right: 20px;
  margin-left: 20px;
}

.unit-details {
  margin-bottom: 69px;
}

#writing-box {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  border-style: outset;
  border-color: #f9f6f0;
  border-radius: 1.2rem;
  padding: 30px;
  margin-left: 0;
  background: rgba(244, 236, 225, 0.5);
  color: #314668;
  height: 530px;
}

#images-box {
  border-color: #faefe0;
  border-radius: 1.2rem;
  border-style: outset;
  /* height: 300px; */
  margin-right: 80px;
  /* padding: 20px; */
  background: rgba(244, 236, 225, 0.5);

}

.v-img:hover {
  -ms-transform: scale(1.5); /* IE 9 */
  -webkit-transform: scale(1.5); /* Safari 3-8 */
  transform: scale(1.5); 
}

.bid-error-msg {
  color: #ff7243;
  font-weight: bold;
}

#bid-history-container {
  display: flex;
  flex-direction: column;
  justify-content: center;
  background-color: rgba(239, 237, 255, 0.8);
}

#bid-amount {
  width: 170px;
  border-radius: .30rem;
  background-color: #f9f6f0;
}

table {
  margin: 10px;
  border-collapse: separate;
}

tbody tr:nth-child(odd) {
  background-color: #8393c2;
  padding: 2rem;
}

tbody tr:nth-child(even) {
  background: rgba(244, 236, 225, 0.2);
}

td {
  padding: 30px;
}

th {
  color: #314668;
  font-size: 1.5rem;
}
</style>
