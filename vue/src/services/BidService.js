import axios from 'axios';

export default {
    bid(user) {
        return axios.put('/bids', user);
    },

    list() {
        return axios.get('/bids');
    },


}