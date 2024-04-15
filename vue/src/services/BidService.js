import axios from 'axios';

export default {
    bid(bid) {
        return axios.post(`/units/${bid.unitId}`, bid);
    },

    list() {
        return axios.get('/bids');
    },

    bidHistory(unitId) {
        return axios.get(`/units/${unitId}/bids`);
    },


}