import axios from 'axios';

export default {
    list() {
        return axios.get('/units');
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

    edit(unit) {
        return axios.put(`/units/${unit.id}/edit`, unit);
    }

}