<template>
    <div id="filter-container">
        <nav>
            <div class="location">
                <h3>Location</h3>
                <input type="checkbox" id="columbus" value="Columbus">
                <label for="Columbus">Columbus</label>
            <br>
                <input type="checkbox" id="cleveland" value="Cleveland">
                <label for="Cleveland">Cleveland</label>
            <br>
                <input type="checkbox" id="cincinnati" value="Cincinnati">
                <label for="Cincinnati">Cincinnati</label>
            <br>
                <input type="checkbox" id="akron" value="Akron">
                <label for="Akron">Akron</label>
            <br>
            </div>

            <div class="expiration">
                <h3>Expires in</h3>
                <input type="checkbox" id="24-hrs" value="1">
                <label for="24-hrs"> &lt; 24hrs</label>
            <br>
                <input type="checkbox" id="3-days" value="3">
                <label for="3-days"> &lt; 3 days</label>
            <br>
                <input type="checkbox" id="7-days" value="7">
                <label for="7-days"> &lt; 7 days</label>
            <br>
            </div>

            <div class="size">
                <h3>Size</h3>
                <input type="checkbox" id="5x5" value="5x5">
                <label for="5x5">5x5</label>
            <br>
                <input type="checkbox" id="10x10" value="10x10">
                <label for="10x10">10x10</label>
            <br>
                <input type="checkbox" id="10x15" value="10x15">
                <label for="10x15">10x15</label>
            <br>
                <input type="checkbox" id="10x20" value="10x20">
                <label for="10x20">10x20</label>
            <br>
            </div>

            <div class="high-bid">
                <h3>High Bid</h3>
                <input type="checkbox" id="0-50" value="0-50">
                <label for="0-50">$0 - $50</label>
            <br>
                <input type="checkbox" id="51-100" value="51-100">
                <label for="51-100">$51 - $100</label>
            <br>
                <input type="checkbox" id="101-150" value="101-150">
                <label for="101-150">$101 - $150</label>
            <br>
                <input type="checkbox" id="150" value="150">
                <label for="150">$151+</label>
            <br>
            </div>
            <button>Apply Filters</button>
        </nav>
    </div>
</template>

<script>
export default {
    data () {
        return {
            filter: {
                location: ['Toledo'],
                //TODO others
            },
        }
    },

    methods: {
        filteredList() {
            let filteredUnits = this.units;
        }
    },
    
}
</script>

<style scoped>
#filter-container {
    display: flex;
    justify-items: flex-start;
    width: 20vw;
    height: 100vh;
    background-color: gray;
}

inputlabel {
    display:flex;
    align-self: left;
}
</style>