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
                <input type="checkbox" id="24-hrs" value="1" v-model="filter.expiration">
                <label for="24-hrs"> &lt; 24hrs</label>
            <br>
                <input type="checkbox" id="3-days" value="3" v-model="filter.expiration">
                <label for="3-days"> &lt; 3 days</label>
            <br>
                <input type="checkbox" id="7-days" value="7" v-model="filter.expiration">
                <label for="7-days"> &lt; 7 days</label>
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
                <input type="checkbox" id="0-50" value="0-50" v-model="filter.highBid">
                <label for="0-50">$0 - $50</label>
            <br>
                <input type="checkbox" id="51-100" value="51-100" v-model="filter.highBid">
                <label for="51-100">$51 - $100</label>
            <br>
                <input type="checkbox" id="101-150" value="101-150" v-model="filter.highBid">
                <label for="101-150">$101 - $150</label>
            <br>
                <input type="checkbox" id="150" value="150" v-model="filter.highBid">
                <label for="150">$151+</label>
            <br>
            </div>
            <button v-on:click="filterUnits">Apply Filters</button>
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
    components: {UnitCard},
    data() {
        return {
            units: [],
            filteredUnits: [],
            filter: {
                location: [],
                expiration: [],
                size: [],
                highBid: [],
            },

        }
    },

    computed: {
    },
    
    methods: {
       
        getUnits(){
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

        filterUnits() {
            
            this.filteredUnits = this.units;
            console.log("Inside filterUnits: ", this.filteredUnits);
            this.filteredUnits = this.filteredUnits.filter(unit => {
                if (this.filter.location.length === 0) {
                    return true;
                }

                return this.filter.location.includes(unit.city); 
            }) 

            // filteredUnits = filteredUnits.filter(unit => {
            //     return this.filter.expiration <= (unit.expiration);
            // })

            this.filteredUnits = this.filteredUnits.filter(unit => {
                if (this.filter.size.length === 0) {
                    return true;
                }

                return this.filter.size.includes(unit.size);
            })

            // filteredUnits = filteredUnits.filter(unit => {
            //     return this.filter.highBid.includes(unit.size);
            // })




            // let locations = this.filter.location;
            // locations.forEach(loc => {
            //     this.units.forEach(unit => {
            //         if(unit.city === loc) {
            //             this.filteredUnits.push(unit);
            //         }
            //     })
            // });
            // // return selected;
        }
    },

    created() {
        UnitService
        .list()
        .then((response) => {
            this.units = response.data;
            
        })
        .catch(); //TODO add generic error handling

        this.getUnits();
    },


}

</script>

<style scoped>
#filter-container{
    display:flex;
    justify-content: left;
    border: 2px solid white;
    width: 20vw;
    height: 100vh;

}

</style>