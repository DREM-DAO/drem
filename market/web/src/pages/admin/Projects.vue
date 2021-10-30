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
            v-if="
              !formShown && project && project.project && project.project.name
            "
          >
            <div class="card">
              <div class="card-header d-flex">
                <h3 class="flex-grow-1">List of photos</h3>
                <button
                  class="btn btn-dark btn-sm"
                  style="width: 100px"
                  @click="this.showPhotoForm = true"
                >
                  +
                </button>
              </div>
            </div>
            <DataTable
              class="p-datatable-sm"
              :value="project.images"
              responsiveLayout="scroll"
              selectionMode="single"
              v-model:selection="imageMeta"
              :paginator="true"
              :rows="5"
            >
              <template #empty> There are no images in the project </template>
              <Column field="name" header="Name" :sortable="true"></Column>
              <Column
                field="description"
                header="Description"
                :sortable="true"
              ></Column>
              <Column field="src" header="src" :sortable="true"></Column>
              <Column
                field="thumbnail"
                header="thumbnail"
                :sortable="true"
              ></Column>
              <Column header="Actions"
                ><template #body="slotProps">
                  <button
                    @click="this.deletePicture(slotProps)"
                    class="btn btn-danger btn-sm"
                  >
                    Delete
                  </button>
                </template></Column
              >
            </DataTable>
          </div>
          <div class="card-body" v-if="showPhotoForm">
            <div class="card">
              <div class="card-header d-flex">
                <h3 class="flex-grow-1">Photo form</h3>
                <button
                  class="btn btn-dark btn-sm"
                  style="width: 100px"
                  @click="
                    this.showPhotoForm = false;
                    this.imageMeta = {};
                  "
                >
                  X
                </button>
              </div>
            </div>
            <div class="card-body">
              <div class="m-1">
                <label for="Name">Name</label>
                <div class="input-group">
                  <input
                    id="Name"
                    class="form-control"
                    v-model="imageMeta.name"
                  />
                </div>
              </div>
              <div class="m-1">
                <label for="Description">Description</label>
                <div class="input-group">
                  <input
                    id="Description"
                    class="form-control"
                    v-model="imageMeta.description"
                  />
                </div>
              </div>
              <div class="m-1">
                <label for="Src">Src</label>
                <div class="input-group">
                  <input
                    id="Src"
                    class="form-control"
                    v-model="imageMeta.src"
                  />
                </div>
              </div>
              <div class="m-1">
                <label for="Thumbnail">Thumbnail</label>
                <div class="input-group">
                  <input
                    id="Thumbnail"
                    class="form-control"
                    v-model="imageMeta.thumbnail"
                  />
                </div>
              </div>
              <div class="m-1">
                <label for="DateOfPicture">DateOfPicture</label>
                <div class="input-group">
                  <Datepicker v-model="imageMeta.dateOfPicture"></Datepicker>
                </div>
              </div>
              <div class="m-1">
                <div class="input-group">
                  <button class="btn btn-primary" @click="savePicture">
                    Save picture metadata
                  </button>
                </div>
              </div>
            </div>
          </div>
          <div
            class="card-body"
            v-if="
              !formShown && project && project.project && project.project.name
            "
          >
            <div class="card">
              <div class="card-header d-flex">
                <h3 class="flex-grow-1">List of buffer txs</h3>

                <button
                  class="btn btn-dark btn-sm"
                  style="width: 100px"
                  @click="this.showBufferForm = true"
                >
                  +
                </button>
              </div>
            </div>
            <DataTable
              class="p-datatable-sm"
              :value="project.bufferTxs"
              responsiveLayout="scroll"
              selectionMode="single"
              v-model:selection="selectedBufferTx"
              :paginator="true"
              :rows="5"
            >
              <template #empty> There are no transfers in the buffer </template>
              <Column field="time" header="Time" :sortable="true"></Column>
              <Column field="note" header="Note" :sortable="true"></Column>
              <Column field="amount" header="Amount" :sortable="true"></Column>
              <Column
                field="currency"
                header="Currency"
                :sortable="true"
              ></Column>
              <Column header="Actions"
                ><template #body="slotProps">
                  <button
                    @click="this.deleteBufferTx(slotProps)"
                    class="btn btn-danger btn-sm"
                  >
                    Delete
                  </button>
                </template></Column
              >
            </DataTable>
          </div>
          <div class="card-body" v-if="showBufferForm">
            <div class="card">
              <div class="card-header d-flex">
                <h3 class="flex-grow-1">Buffer transaction form</h3>
                <button
                  class="btn btn-dark btn-sm"
                  style="width: 100px"
                  @click="
                    this.showBufferForm = false;
                    this.selectedBufferTx = {};
                  "
                >
                  X
                </button>
              </div>
            </div>
            <div class="card-body">
              <div class="m-1">
                <label for="Note">Note</label>
                <div class="input-group">
                  <input
                    id="Note"
                    class="form-control"
                    v-model="selectedBufferTx.note"
                  />
                </div>
              </div>
              <div class="m-1">
                <label for="Amount">Amount</label>
                <div class="input-group">
                  <input
                    id="Amount"
                    class="form-control"
                    v-model="selectedBufferTx.amount"
                  />
                </div>
              </div>
              <div class="m-1">
                <label for="Currency">Currency</label>
                <div class="input-group">
                  <input
                    id="Currency"
                    class="form-control"
                    v-model="selectedBufferTx.currency"
                  />
                </div>
              </div>
              <div class="m-1">
                <label for="DateOfPicture">Time</label>
                <div class="input-group">
                  <Datepicker v-model="selectedBufferTx.time"></Datepicker>
                </div>
              </div>
              <div class="m-1">
                <div class="input-group">
                  <button class="btn btn-primary" @click="saveBufferTx">
                    Save tx data
                  </button>
                </div>
              </div>
            </div>
          </div>
          <div
            class="card-body"
            v-if="
              !formShown && project && project.project && project.project.name
            "
          >
            <div class="card">
              <div class="card-header d-flex">
                <h3 class="flex-grow-1">List of daily payouts</h3>
              </div>
            </div>
            <DataTable
              class="p-datatable-sm"
              :value="project.dailyPayouts"
              responsiveLayout="scroll"
              selectionMode="single"
              v-model:selection="selectedDailyPayout"
              :paginator="true"
              :rows="5"
            >
              <template #empty> There are no daily payouts </template>
              <Column field="name" header="Name" :sortable="true"></Column>
              <Column
                field="description"
                header="description"
                :sortable="true"
              ></Column>
              <Column field="src" header="src" :sortable="true"></Column>
              <Column
                field="thumbnail"
                header="thumbnail"
                :sortable="true"
              ></Column>
            </DataTable>
          </div>
          <div
            class="card-body"
            v-if="
              !formShown && project && project.project && project.project.name
            "
          >
            <div class="card">
              <div class="card-header d-flex">
                <h3 class="flex-grow-1">List of shareholders</h3>
              </div>
            </div>
            <DataTable
              class="p-datatable-sm"
              :value="project.shareholders"
              responsiveLayout="scroll"
              selectionMode="single"
              v-model:selection="selectedShareholder"
              :paginator="true"
              :rows="5"
            >
              <template #empty> There are no shareholders </template>
              <Column field="name" header="Name" :sortable="true"></Column>
              <Column
                field="description"
                header="description"
                :sortable="true"
              ></Column>
              <Column field="src" header="src" :sortable="true"></Column>
              <Column
                field="thumbnail"
                header="thumbnail"
                :sortable="true"
              ></Column>
            </DataTable>
          </div>
        </div>
      </div>
    </div>
  </MainLayout>
</template>

<script>
import MainLayout from "../../layouts/Main.vue";
import Datepicker from "vue3-date-time-picker";
import "vue3-date-time-picker/dist/main.css";
import { mapActions } from "vuex";
export default {
  components: {
    MainLayout,
    Datepicker,
  },

  data() {
    return {
      imageMeta: {},
      project: null,
      projects: [],
      selectedProject: null,
      codes: [],
      valueset: {},
      selected: "",
      selectedKey: "",
      itemText: "",
      value: "",
      selectedBufferTx: {},
      selectedDailyPayout: null,
      selectedShareholder: null,
      showPhotoForm: false,
      showBufferForm: false,
      showPayoutsForm: false,
      showShareholderForm: false,
    };
  },
  watch: {
    project() {
      console.log("project", this.project);
    },
    selectedProject() {
      this.loadProject();
    },
    imageMeta() {
      if (this.imageMeta && this.imageMeta.id) {
        this.showPhotoForm = true;
      }
      if (!this.imageMeta) {
        this.imageMeta = {};
      }
    },
    selectedBufferTx() {
      if (this.selectedBufferTx && this.selectedBufferTx.id) {
        this.showBufferForm = true;
      }
      if (!this.selectedBufferTx) {
        this.selectedBufferTx = {};
      }
    },
  },
  computed: {
    formShown() {
      return (
        this.showPhotoForm ||
        this.showBufferForm ||
        this.showPayoutsForm ||
        this.showShareholderForm
      );
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
      axiosPut: "axios/put",
      axiosDelete: "axios/delete",
      openError: "toast/openError",
      openSuccess: "toast/openSuccess",
    }),
    async loadProject() {
      if (this.selectedProject && this.selectedProject.urlId) {
        this.project = await this.axiosGet({
          url: `${this.$store.state.config.dremapi}/Project/GetDetail/${this.selectedProject.urlId}`,
        });
      }
    },
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
    async savePicture() {
      if (!this.selectedProject || !this.selectedProject.id) {
        this.openError("You must select project first");
        return;
      }
      if (this.imageMeta.id) {
        this.imageMeta.projectId = this.selectedProject.id;
        const ret = await this.axiosPut({
          url: `${this.$store.state.config.dremapi}/ImageMeta/Update/${this.imageMeta.id}`,
          body: this.imageMeta,
        });
        this.closeForm();
        if (ret) {
          this.openSuccess("Picture updated");
        } else {
          this.openError("Error updating picture");
        }
      } else {
        this.imageMeta.projectId = this.selectedProject.id;
        const ret = await this.axiosPost({
          url: `${this.$store.state.config.dremapi}/ImageMeta/Create`,
          body: this.imageMeta,
        });
        this.closeForm();
        if (ret) {
          this.openSuccess("Picture created");
        } else {
          this.openError("Error creating picture");
        }
      }
      this.loadProject();
    },
    async deletePicture(item) {
      console.log("item", item);
      if (!item || !item.data || !item.data.id) {
        this.openError("You must select image first");
        return;
      }
      if (!confirm("Are you sure?")) return;
      const ret = await this.axiosDelete({
        url: `${this.$store.state.config.dremapi}/ImageMeta/Delete/${item.data.id}`,
      });
      this.closeForm();
      if (ret) {
        this.openSuccess("Picture deleted");
      } else {
        this.openError("Error deleting picture");
      }
      this.loadProject();
    },
    async saveBufferTx() {
      if (!this.selectedProject || !this.selectedProject.id) {
        this.openError("You must select project first");
        return;
      }
      if (this.selectedBufferTx.id) {
        this.selectedBufferTx.projectId = this.selectedProject.id;
        const ret = await this.axiosPut({
          url: `${this.$store.state.config.dremapi}/BufferTransfer/Update/${this.selectedBufferTx.id}`,
          body: this.selectedBufferTx,
        });
        this.closeForm();
        if (ret) {
          this.openSuccess("BufferTransfer updated");
        } else {
          this.openError("Error updating bufferTransfer");
        }
      } else {
        this.selectedBufferTx.projectId = this.selectedProject.id;
        const ret = await this.axiosPost({
          url: `${this.$store.state.config.dremapi}/BufferTransfer/Create`,
          body: this.selectedBufferTx,
        });
        this.closeForm();
        if (ret) {
          this.openSuccess("BufferTransfer created");
        } else {
          this.openError("Error creating bufferTransfer");
        }
      }
      this.loadProject();
    },
    async deleteBufferTx(item) {
      console.log("item", item);
      if (!item || !item.data || !item.data.id) {
        this.openError("You must select image first");
        return;
      }
      if (!confirm("Are you sure?")) return;
      const ret = await this.axiosDelete({
        url: `${this.$store.state.config.dremapi}/BufferTransfer/Delete/${item.data.id}`,
      });
      this.closeForm();
      if (ret) {
        this.openSuccess("BufferTransfer deleted");
      } else {
        this.openError("Error deleting bufferTransfer");
      }
      this.loadProject();
    },

    closeForm() {
      this.imageMeta = {};
      this.showPhotoForm = false;
      this.selectedBufferTx = {};
      this.showBufferForm = false;
    },
  },
};
</script>