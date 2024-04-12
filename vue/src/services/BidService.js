import axios from 'axios';

export default {
    bid(bid) {
        return axios.post('/bid', bid);
    },

    list() {
        return axios.get('/bids');
    },


}