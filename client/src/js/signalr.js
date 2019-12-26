import * as signalR from '@microsoft/signalr'
import api from './api/api'

const SignalR = {
  install: function (Vue) {
    Vue.prototype.$signalR = {
      connect(hubName) {
        return new signalR.HubConnectionBuilder()
          .withUrl(`${new URL(api.baseUrl)}${hubName}`)
          .withAutomaticReconnect()
          .build();
      },
    };
  },
};

export default SignalR
