import axios from 'axios';

export default {
    bid(thisBid) {
        return axios.post(`/units/${unitId}`, user);
    },

    list() {
        return axios.get('/bids');
    },


}