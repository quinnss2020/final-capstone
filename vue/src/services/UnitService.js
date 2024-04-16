import axios from 'axios';

export default {
    list() {
        return axios.get('/units');
    },

    listAll() {
        return axios.get('/units/all');
    },

    unitDetails(id) {
        return axios.get(`/units/${id}`);
    },

    bidHistory(id) {
        return axios.get(`/units/${id}/bids`);
    },

    endAuction() {
        return axios.put('/units/checkout');
    },

}