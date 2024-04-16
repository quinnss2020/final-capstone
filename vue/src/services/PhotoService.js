import axios from 'axios';

export default {
    getPhotos(unitId){
        return axios.post(`/units/${unitId}/photos`)
    },
}