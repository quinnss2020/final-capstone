<template>
    <br>
    <div id="view-units">
        <!-- <h1>Available Units</h1> -->

        <body class="page-container">
            <div class="wrapper">
                <aside>
                    <div class="location">
                        <h2>Filter Units</h2>
                        <br>
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
                    <br>
                    <v-btn elevation="8" rounded="xl" size="regular" v-on:click="filterUnits">APPLY</v-btn>
                    <v-btn elevation="8" rounded="xl" size="regular" v-on:click="clearFilters">CLEAR</v-btn>
                </aside>
                <main id="unit-cards-container">
                    <div id="unit-cards">
                        <UnitCard v-for="unit in filterUnits" v-bind:key="unit.id" v-bind:item="unit" />
                    </div>
                </main>
            </div>
        </body>
        <!-- <div id="page-container">
            <div id="filter-container">
            <nav>
                <div class="location">
                    <h2>Filter Units</h2>
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
                <br>
                <button v-on:click="filterUnits">APPLY</button>
                <button v-on:click="clearFilters">CLEAR</button>
            </nav>
        </div>
        <div>
            <section class="unit-container">
                <UnitCard v-for="unit in filteredUnits" v-bind:key="unit.id" v-bind:item="unit" />
            </section>
            </div>
        </div> -->
    </div>
</template>

<script>
import UnitCard from '../components/UnitCard.vue';
import UnitService from '../services/UnitService.js';


export default {
    name: "ListUnits",
    components: { UnitCard },
    data() {
        return {
            units: [],
            // filteredUnits: [],
            filter: {
                location: [],
                expiration: [],
                size: [],
                highestBid: [],
            },
        }
    },

    computed: {
        filterUnits() {

            let filteredUnits = this.units;

            filteredUnits = filteredUnits.filter(unit => {
                if (this.filter.location.length === 0) {
                    return true;
                }
                return this.filter.location.includes(unit.city);
            })
            //TODO see if we can call countdown util below
            filteredUnits = filteredUnits.filter(unit => {
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

            filteredUnits = filteredUnits.filter(unit => {
                if (this.filter.size.length === 0) {
                    return true;
                }
                return this.filter.size.includes(unit.size);
            })


            filteredUnits = filteredUnits.filter(unit => {
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
            return filteredUnits;
        },
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

        clearFilters() {
            this.filter = {
                location: [],
                expiration: [],
                size: [],
                highestBid: [],
            }
            this.getUnits();

            return this.filter;
        }

    },

    created() {
        this.getUnits();
        // UnitService
        //     .list()
        //     .then((response) => {
        //         this.units = response.data;

        //     })
        //     .catch((error) => {
        //         if (error.response) {
        //             console.log("Error loading units in <CREATED>: ", error.response.status);
        //         }
        //         else if (error.request) {
        //             console.log("Error loading units in <CREATED>. Unable to communicate to server.");
        //         }
        //         else {
        //             console.log("Error making request in <CREATED>");
        //         }
        //     });
    },


}


</script>

<style scoped>
.page-container {
    display: flex;
    overflow: hidden;
    height: 100 vh;
}

#unit-cards-container {
    /* display: flex; */
    justify-content: flex-start;
    flex-direction: row;
    overflow-y: auto;
    margin-left: 330px;
    padding: 0px 10px;

}

#unit-cards {
    display: flex;
    flex-wrap: wrap;
}

aside {
    width: 300px;
    position: fixed;
    z-index: 1;
    top: 150px;
    left: 30px;
    overflow-x: hidden;
    padding-top: 10px;
    padding-left: 30px;
    padding-right: px;
    padding-bottom: 15px;
    text-align: left;
    border-color: #faefe0;
    background: rgba(244, 236, 225, .5);
    border-radius: 2rem;
    border-style: outset;
}

/* .unit-cards {
    display: flex;
    flex-wrap: wrap;
    justify-content: flex-start;
    width: 600px;
    height: 100vh
} */

/* #page-container {
    display: flex;
    flex-direction: row;
    
} */
/* 
#filter-container {
   overflow: hidden;
    /* display: flex;
    justify-content: left;
    border: 2px solid white;
    height: 100vh;
    flex-direction: column;
    padding-left: 20px;
    border-color: #faefe0;
    border-radius: 2rem;
    border-style: outset;
    margin-left: 50px
 /* } */
/* #filter-container {
    overflow: hidden;
}

#filter-container>nav {
    text-align: left;
    padding-bottom: 30px;
    border-color: #faefe0;
    border-radius: 2rem;
    border-style: outset;
    padding-left: 20px;
    padding-right: 20px;
    background: rgba(244, 236, 225, .5);
    margin-left: 50px;
}

#unit-container {
    display: flex;
    flex-wrap: wrap;
    justify-content: flex-start;
    width: 600px;
    height: 100vh;
    flex-direction: row;
    overflow-y: auto;
}

.unit-container {
    display: flex;
    flex-wrap: wrap;
} */

h3 {
    color: #706d57;
}

button {
    width: 115px;
    margin-right: 10px;
}
</style>