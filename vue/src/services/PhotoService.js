import axios from 'axios';

export default {
    addImage(imageData){
        return axios.post(`/units/:unitId/images`)
    }
}