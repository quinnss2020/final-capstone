import axios from 'axios';

export default {
    getPhotos(unitId){
        return axios.get(`/units/${unitId}/photos`)
    },
    postPhoto(photo){
        return axios.post(`/units/${photo.unitId}/photos/add`, photo)
    }
}