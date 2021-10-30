<template>
  <MainLayout>
    <h1>Manage projects</h1>

    <div class="row" v-if="!selectedProject">
      <div class="col">
        <div class="card">
          <div class="card-header">
            <h2>List of projects</h2>
          </div>

          <DataTable
            :value="projects"
            responsiveLayout="scroll"
            selectionMode="single"
            v-model:selection="selectedProject"
            :paginator="true"
            :rows="20"
          >
            <template #empty> No projects </template>
            <Column field="asa" header="AsaId" :sortable="true"></Column>
            <Column field="name" header="Name" :sortable="true"></Column>
          </DataTable>
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
    <div class="row" v-if="selectedProject">
      <div class="col">
        <div class="card">
          <div class="card-header d-flex">
            <h2 class="flex-grow-1">
              Project:
              <span v-if="project && project.project && project.project.name">
                {{ project.project.name }}
              </span>
              <span
                v-else
                class="spinner-grow spinner-grow-sm"
                role="status"
                aria-hidden="true"
              ></span>
            </h2>
            <button
              class="btn btn-dark btn-sm"
              style="width: 100px"
              @click="this.selectedProject = null"
            >
              X
            </button>
          </div>
          <div
            class="card-body"
            v-if="project && project.project && project.project.name"
          >
            <div class="card">
              <div class="card-header d-flex">
                <h3 class="flex-grow-1">List of photos:</h3>
              </div>
            </div>
            <DataTable
              :value="projects"
              responsiveLayout="scroll"
              selectionMode="single"
              v-model:selection="selectedProject"
              :paginator="true"
              :rows="20"
            >
              <template #empty> No projects </template>
              <Column field="asa" header="AsaId" :sortable="true"></Column>
              <Column field="name" header="Name" :sortable="true"></Column>
            </DataTable>
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
      project: null,
      projects: [],
      selectedProject: null,
      codes: [],
      valueset: {},
      selected: "",
      selectedKey: "",
      itemText: "",
      value: "",
    };
  },
  watch: {
    project() {
      console.log("project", this.project);
    },
    async selectedProject() {
      if (this.selectedProject && this.selectedProject.urlId) {
        this.project = await this.axiosGet({
          url: `${this.$store.state.config.dremapi}/Project/GetDetail/${this.selectedProject.urlId}`,
        });
      }
    },
  },

  async mounted() {
    await this.prolong();
    this.projects = await this.axiosGet({
      url: `${this.$store.state.config.dremapi}/Project/ListAll`,
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