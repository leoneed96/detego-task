  import axios from "axios";
  import api from "./api";
  export default {
      async getAll(){
          try {
            return (await axios.get(api.constructUrl("crud/getRecords"))).data;
          }
          catch(e){
              throw e;
          }
      },
      async removeRecord(id){
        try {
          return (await axios.post(api.constructUrl("crud/removeRecord"), id))
        }
        catch(e){
            throw e;
        }
    },
  }