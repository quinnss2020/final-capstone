<template>
    <div id="view-units">
        <h1>This is the Unit View</h1>
        <div id="filter-container">
            <nav>
                <div class="location">
                    <h3>Location</h3>
                    <input type="checkbox" id="columbus" value="Columbus" v-model="filter.location">
                    <label for="Columbus">Columbus</label>
                    <br>
                    <input type="checkbox" id="cleveland" value="Cleveland" v-model="filter.location">
                    <label for="Cleveland">Cleveland</label>
                    <br>
                    <input type="checkbox" id="cincinnati" value="Cincinnati" v-model="filter.location">
                    <label for="Cincinnati">Cincinnati</label>
                    <br>
                    <input type="checkbox" id="akron" value="Akron" v-model="filter.location">
                    <label for="Akron">Akron</label>
                    <br>
                </div>

                <div class="expiration">
                    <h3>Expires in</h3>
                    <input type="radio" id="one-hour" value="60" v-model="filter.expiration">
                    <label for="one-hour"> &lt; 1 hour </label>
                    <br>
                    <input type="radio" id="three-hours" value="180" v-model="filter.expiration">
                    <label for="three-hours"> &lt; 3 hours</label>
                    <br>
                    <input type="radio" id="twentyfour-hours" value="1440" v-model="filter.expiration">
                    <label for="twentyfour-hours"> &lt; 24 hours</label>
                    <br>
                </div>

                <div class="size">
                    <h3>Size</h3>
                    <input type="checkbox" id="5x5" value="5x5" v-model="filter.size">
                    <label for="5x5">5x5</label>
                    <br>
                    <input type="checkbox" id="10x10" value="10x10" v-model="filter.size">
                    <label for="10x10">10x10</label>
                    <br>
                    <input type="checkbox" id="10x15" value="10x15" v-model="filter.size">
                    <label for="10x15">10x15</label>
                    <br>
                    <input type="checkbox" id="10x20" value="10x20" v-model="filter.size">
                    <label for="10x20">10x20</label>
                    <br>
                </div>

                <div class="high-bid">
                    <h3>High Bid</h3>
                    <input type="checkbox" id="0-50" value="0-50" v-model="filter.highestBid">
                    <label for="0-50">$0 - $50</label>
                    <br>
                    <input type="checkbox" id="51-100" value="51-100" v-model="filter.highestBid">
                    <label for="51-100">$51 - $100</label>
                    <br>
                    <input type="checkbox" id="101-150" value="101-150" v-model="filter.highestBid">
                    <label for="101-150">$101 - $150</label>
                    <br>
                    <input type="checkbox" id="150" value="151-200" v-model="filter.highestBid">
                    <label for="150">$151 - $200</label>
                    <br>
                </div>
                <button v-on:click="filterUnits">Apply Filters</button>
                <button v-on:click="clearFilters">Clear Filters</button>
            </nav>
        </div>


        <div>

        </div>
        <section class="unit-container">
            <UnitCard v-for="unit in filteredUnits" v-bind:key="unit.id" v-bind:item="unit" />
        </section>
    </div>
</template>

<script>
import UnitCard from '../components/UnitCard.vue';
import UnitService from '../services/UnitService.js';
// import FilterUnits from '../components/FilterUnits.vue'

export default {
    name: "ListUnits",
    components: { UnitCard },
    data() {
        return {
            filteredUnits: [],
            filter: {
                location: [],
                expiration: [],
                size: [],
                highestBid: [],
            },

        }
    },

    computed: {
    },

    methods: {

        getUnits() {
            UnitService
                .list()
                .then((response) => {
                    this.filteredUnits = response.data;
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

        filterUnits() {

            this.filteredUnits = this.units;

            this.filteredUnits = this.filteredUnits.filter(unit => {
                if (this.filter.location.length === 0) {
                    return true;
                }
                return this.filter.location.includes(unit.city);
            })

            this.filteredUnits = this.filteredUnits.filter(unit => {
                if (this.filter.expiration.length === 0) {
                    return true;
                }
                var date = new Date(Date.parse(unit.expiration));
                const now = Date.now();
                let timeRemaining = date - now;
                var minutes = Math.floor(timeRemaining / 60000)
                var seconds = Math.floor((timeRemaining % (1000 * 60)) / 1000);
                console.log('reached expiration filter')
                return minutes < (this.filter.expiration) && seconds > 0;

            })

            this.filteredUnits = this.filteredUnits.filter(unit => {
                if (this.filter.size.length === 0) {
                    return true;
                }
                return this.filter.size.includes(unit.size);
            })


            this.filteredUnits = this.filteredUnits.filter(unit => {
                if (this.filter.highestBid.length === 0) {
                    return true;
                }

                let result;

                this.filter.highestBid.forEach((x) => {
                    let range = x.split("-");
                    result = unit.highestBid >= parseInt(range[0]) && unit.highestBid <= parseInt(range[1]);
                })

                return result;
            })
        },

        clearFilters() {
            if (this.filter.location || this.filter.expiration || this.filter.size || this.filter.highestBid){
                this.filter = {};
            }

            return this.filter;
        }

    },

    created() {
        UnitService
            .list()
            .then((response) => {
                this.units = response.data;

            })
            .catch((error) => {
                if (error.response) {
                    console.log("Error loading units in <CREATED>: ", error.response.status);
                }
                else if (error.request) {
                    console.log("Error loading units in <CREATED>. Unable to communicate to server.");
                }
                else {
                    console.log("Error making request in <CREATED>");
                }
            });

        this.getUnits();
    },


}

</script>

<style scoped>
#filter-container {
    display: flex;
    justify-content: left;
    border: 2px solid white;
    width: 20vw;
    height: 100vh;

}
</style>