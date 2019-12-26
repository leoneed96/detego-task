  import axios from "axios";
  import api from "./api";
  export default {
      async activate(){
          try {
            return (await axios.post(api.constructUrl("rfid/activate"))).data;
          }
          catch(e){
              throw e;
          }
      },
      async getStatus(){
          try {
            return (await axios.get(api.constructUrl("rfid/status"))).data;
          }
          catch(e){
              throw e;
          }
      },
      async deactivate(){
        try {
          return (await axios.post(api.constructUrl("rfid/deactivate"))).data
        }
        catch(e){
            throw e;
        }
    },
  }