<template>
  <MainLayout>
    <h1>Manage value sets</h1>

    <div class="row">
      <div class="col">
        <div class="card">
          <div class="card-header">
            <h2>List of value sets</h2>
          </div>

          <ul class="list-group">
            <li
              class="list-group-item"
              :class="this.selected == item ? 'active' : ''"
              v-for="item in codes"
              :key="item"
              @click="this.selected = item"
            >
              {{ item }}
            </li>
          </ul>
          <div class="card-body">
            <div class="mb-3">
              <label for="selected" class="form-label">ValueSet code</label>
              <input id="selected" v-model="selected" class="form-control" />
            </div>
          </div>
        </div>
      </div>
      <div v-if="selected && valueset" class="col">
        <div class="card">
          <div class="card-header">
            <h2>Value set {{ selected }}</h2>
          </div>
          <ul class="list-group">
            <li
              class="list-group-item"
              :class="this.selectedKey == key ? 'active' : ''"
              v-for="key in Object.keys(valueset)"
              :key="key"
              @click="
                this.selectedKey = key;
                this.itemText = this.valueset[key];
              "
            >
              {{ this.valueset[key] }}
            </li>
          </ul>
        </div>
      </div>

      <div v-if="selected" class="col">
        <div class="card">
          <div class="card-header">
            <h2>Set valueset value</h2>
          </div>
          <div class="card-body">
            <div class="mb-3">
              <label for="itemCode" class="form-label">Item code</label>
              <input id="itemCode" v-model="selectedKey" class="form-control" />
            </div>
            <div class="mb-3">
              <label for="itemText" class="form-label">Item text</label>
              <input id="itemText" v-model="itemText" class="form-control" />
            </div>
            <div class="mb-3">
              <button class="btn btn-primary" @click="setValue">
                Set value
              </button>
              <button class="btn btn-danger" @click="deleteItem">
                Delete item
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </MainLayout>
</template>

<script>
import MainLayout from "../../layouts/Main.vue";
import { mapActions } from "vuex";
export default {
  components: {
    MainLayout,
  },

  data() {
    return {
      codes: [],
      valueset: {},
      selected: "",
      selectedKey: "",
      itemText: "",
      value: "",
    };
  },
  watch: {
    async selected() {
      this.valueset = await this.axiosGet({
        url: `${this.$store.state.config.dremapi}/ValueSet/Get/${this.selected}`,
      });
      console.log("this.valueset", this.valueset);
    },
    selectedKey() {
      console.log("selectedKey", this.selectedKey);
    },
  },

  async mounted() {
    await this.prolong();
    this.codes = await this.axiosGet({
      url: this.$store.state.config.dremapi + "/ValueSet/List",
    });
  },
  methods: {
    ...mapActions({
      prolong: "wallet/prolong",
      axiosGet: "axios/get",
      axiosPost: "axios/post",
    }),
    async setValue() {
      const data = {
        valueSetCode: this.selected,
        itemCode: this.selectedKey,
        ItemValue: this.itemText,
      };
      console.log("data", data);
      await this.axiosPost({
        url: `${this.$store.state.config.dremapi}/ValueSet/Set`,
        body: data,
      });
      this.codes = await this.axiosGet({
        url: this.$store.state.config.dremapi + "/ValueSet/List",
      });
      this.valueset = await this.axiosGet({
        url: `${this.$store.state.config.dremapi}/ValueSet/Get/${this.selected}`,
      });
    },
    async deleteItem() {
      const data = {};
      console.log("data", data);
      await this.axiosPost({
        url: `${this.$store.state.config.dremapi}/ValueSet/DeleteItem/${this.selected}/${this.selectedKey}`,
        body: data,
      });
      this.codes = await this.axiosGet({
        url: this.$store.state.config.dremapi + "/ValueSet/List",
      });
      this.valueset = await this.axiosGet({
        url: `${this.$store.state.config.dremapi}/ValueSet/Get/${this.selected}`,
      });
    },
  },
};
</script>