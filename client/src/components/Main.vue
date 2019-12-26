<template>
  <v-container ref="container" v-resize="onResize" fluid grid-list-md>
    <v-layout row>
      <v-flex xs12 ref="statePanel">
        <v-card>
          <v-list-item three-line>
            <v-list-item-content>
              <div class="overline mb-4">READER STATE PANEL</div>
              <v-list-item-title class="headline mb-1">Reader</v-list-item-title>
              <v-list-item-title>
                State:
                <v-chip :color="stateData.color">{{stateData.status}}</v-chip>
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>
          <v-card-actions>
            <v-btn @click="deactivate" text v-if="readerState.isActive">Deactivate</v-btn>
            <v-btn @click="activate" text v-else>Activate</v-btn>
            <v-btn @click="refreshState" text>Refresh state</v-btn>
            <v-btn @click="refreshHistory" text>Refresh history</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
      <v-flex xs12>
        <v-card :style="'overflow-y:auto; max-height:' + listHeight" :loading="!historyLoaded">
          <v-card-title>History</v-card-title>
          <v-list>
            <v-list-item v-for="(item, idx) in history" :key="idx">
              <v-list-item-content>
                <v-layout row>
                  <v-flex lg4 xs8>
                    <v-list-item-title>{{item.id}}</v-list-item-title>
                    <v-list-item-subtitle>Read at: {{getReadableDate(item.readDate)}}</v-list-item-subtitle>
                  </v-flex>
                  <v-flex lg4 xs4>
                    <v-chip :color="item.seenCount > 3 ? 'green' : ''">seen: {{item.seenCount}}</v-chip>
                  </v-flex>
                </v-layout>
              </v-list-item-content>
            </v-list-item>
          </v-list>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import rfidApi from "../js/api/rfid";
import crudApi from "../js/api/crud";
import arrayMove from "array-move";
export default {
  name: "Main",
  data: () => ({
    readerState: {
      isActive: false,
      status: {}
    },
    historyLoaded: false,
    history: [],
    listHeight: "auto"
  }),
  computed: {
    stateData() {
      switch (this.readerState.status) {
        case 0:
          return { status: "Activated", color: "green" };
        case 1:
          return { status: "Dectivated", color: "yellow" };
        case 2:
          return { status: "Error", color: "red" };
          
        default: return { status: "Unknown", color: "gray" };
      }
    },
  },
  methods: {
    async activate() {
      this.readerState = await rfidApi.activate();
    },
    async refreshState() {
      try {
        this.readerState = await rfidApi.getStatus();
      } catch (e) {
        this.readerState.status = 2;
        console.error(e);
      }
    },
    async refreshHistory() {
      try {
        this.historyLoaded = false;
        this.history = await crudApi.getAll();
      } finally {
        setTimeout(() => {
          this.historyLoaded = true;
        }, 1200);
      }
    },
    async deactivate() {
      this.readerState = await rfidApi.deactivate();
    },
    getReadableDate(isoDateString) {
      return new Date(isoDateString).toLocaleString();
    },
    onTagSeen(tag) {
      let tagInList = this.history.find(item => item.id == tag.transponder.id);
      if (tagInList) {
        tagInList.seenCount = tag.transponder.seenCount;
        tagInList.readDate = tag.readDate;
        this.history = arrayMove(
          this.history,
          this.history.indexOf(tagInList),
          0
        );
      } else {
        tagInList = {
          id: tag.transponder.id,
          seenCount: tag.transponder.seenCount,
          readDate: tag.readDate
        };
        this.history.unshift(tagInList);
      }
    },
    onResize(){
      let containerHeight = window.innerHeight - this.$refs.container.getBoundingClientRect().y;
      let statePanelHeight = this.$refs.statePanel.clientHeight;
      this.listHeight = (containerHeight - statePanelHeight - 20) + "px";
    }
  },
  async mounted() {
    await this.refreshHistory();
    await this.refreshState();
    const connection = this.$signalR.connect("tag");
    connection.start();
    connection.on("ReceiveMessage", (event, tag) => this.onTagSeen(tag));
  }
};
</script>
