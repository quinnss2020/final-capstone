import axios from 'axios';

export default {
    getPhotos(unitId){
        return axios.get(`/units/${unitId}/photos`)
    }
}