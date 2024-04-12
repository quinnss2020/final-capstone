import axios from 'axios';

export default {
    bid(bid) {
        return axios.post(`/units/${bid.unitId}`, bid);
    },

    list() {
        return axios.get('/bids');
    },


}