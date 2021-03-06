<template>
  <PublicLayout>
    <div class="row row-cols-lg-3">
      <div class="col">
        <h1>{{ project.name }}</h1>
        <Badge class="m-1"> 3 bedrooms </Badge>
        <Badge class="m-1"> 1 bathroom </Badge>
        <Badge class="m-1"> 924 sqft </Badge>
        <Badge class="m-1"> Built in 1920 </Badge>
        <Badge class="m-1"> Single Family </Badge>
        <div class="row">
          <div class="col">
            <table class="w-100">
              <tr>
                <td>Address</td>
                <td>{{ project.address }}</td>
              </tr>
              <tr>
                <td>GPS</td>
                <td>
                  Lat: {{ this.$filters.formatGps(project.lat) }}<br />
                  Lng: {{ this.$filters.formatGps(project.lng) }}
                </td>
              </tr>
              <tr>
                <td>IRR</td>
                <td>{{ this.$filters.formatPercent(this.project.rate, 4) }}</td>
              </tr>
              <tr>
                <td>NFT #id</td>
                <td>
                  <a
                    target="_blank"
                    rel="norefferer"
                    :href="`https://testnet.algoexplorer.io/asset/${project.asa}`"
                    >{{ this.getAssetName(project.asa) }} ({{ project.asa }})</a
                  ><br />

                  <a
                    target="_blank"
                    rel="norefferer"
                    :href="`https://testnet.algodex.com/trade/${project.asa}`"
                    >AlgoDex market</a
                  >
                  <div v-if="asset">
                    Decimals: {{ this.getAssetDecimals(this.project.asa) }}
                  </div>
                </td>
              </tr>
              <tr v-if="account && account.addr">
                <td>Your algo balance</td>
                <td>{{ this.$filters.formatCurrency(this.info.amount) }}</td>
              </tr>
              <tr v-if="asset">
                <td>Your asset balance</td>
                <td>
                  {{
                    this.$filters.formatCurrency(
                      this.getAccountAssetBalance(),
                      this.getAssetName(project.asa),
                      this.getAssetDecimals(this.project.asa)
                    )
                  }}
                </td>
              </tr>
            </table>
          </div>
          <div class="col">
            <div class="card card-primary">
              <div
                class="card-header"
                :class="
                  order.isOffer
                    ? 'bg-danger text-white'
                    : 'bg-success text-white'
                "
              >
                Place order
                <div class="form-check form-switch float-end">
                  <input
                    v-model="order.isOffer"
                    class="form-check-input"
                    type="checkbox"
                    id="flexSwitchCheckDefault"
                  />
                  <label class="form-check-label" for="flexSwitchCheckDefault"
                    >Buy / Sell</label
                  >
                </div>
              </div>
              <div class="card-body">
                <div class="">
                  <div class="mb-1 row row-cols-lg-2">
                    <div class="col col-lg-4">
                      <label for="price" class="form-label">Price</label>
                    </div>
                    <div class="col col-lg-8">
                      <input
                        v-model="order.price"
                        class="form-control"
                        id="price"
                        placeholder="Order price"
                        type="number"
                        step="0.000001"
                      />
                    </div>
                  </div>
                  <div class="mb-1 row row-cols-lg-2">
                    <div class="col col-lg-4">
                      <label for="quantity" class="form-label">Quantity</label>
                    </div>
                    <div class="col col-lg-8">
                      <input
                        v-model="order.quantity"
                        class="form-control"
                        id="quantity"
                        placeholder="Order quantity"
                        type="number"
                        step="0.000001"
                      />
                    </div>
                  </div>
                  <div class="mb-1 row row-cols-lg-2">
                    <div class="col offset-lg-4 col-lg-8">
                      <div v-if="$store.state.wallet.isOpen">
                        <div
                          v-if="orderstate && processingOrder"
                          class="alert alert-info"
                        >
                          {{ orderstate }}
                        </div>
                        <div v-else>
                          <button
                            :disabled="processingOrder"
                            class="btn btn-primary"
                            v-if="order.isOffer"
                            @click="makerSell"
                          >
                            Maker sell
                          </button>
                          <button
                            :disabled="processingOrder"
                            class="btn btn-primary"
                            v-if="!order.isOffer"
                            @click="makerBuy"
                          >
                            Maker buy
                          </button>
                        </div>
                      </div>
                      <div v-else>
                        <button
                          class="btn btn-primary"
                          @click="this.$router.push('/accounts/')"
                        >
                          Open wallet
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="card-footer" v-if="orderAlgoAmount">
                Order algo amount:
                {{ this.$filters.formatCurrency(this.orderAlgoAmount) }}
              </div>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col">
            <h4 class="text-end">Bids</h4>
            <DataTable
              v-if="orders && orders.buyASAOrdersInEscrow"
              :value="orders.buyASAOrdersInEscrow"
              class="p-datatable-sm"
              responsiveLayout="scroll"
              :paginator="true"
              :rows="5"
              sort-field="asaPrice"
              :sort-order="-1"
              stripedRows
              :rowClass="myOrderRow"
            >
              <Column field="formattedPrice" header="">
                <template #body="slotProps">
                  <span>
                    <a
                      class="curier"
                      target="_blank"
                      :href="`https://testnet.algoexplorer.io/address/${slotProps.data.ownerAddress}`"
                      >{{ slotProps.data.ownerAddress.substring(0, 2) }}</a
                    >
                  </span>
                  <span
                    class="btn btn-light btn-sm mx-1"
                    v-if="
                      slotProps.data.ownerAddress ===
                      this.$store.state.wallet.lastActiveAccount
                    "
                    @click="cancelBuy(slotProps.data)"
                  >
                    X
                  </span>
                  <span
                    class="btn btn-light btn-sm mx-1"
                    v-if="
                      this.order.quantity &&
                      slotProps.data.ownerAddress ===
                        this.$store.state.wallet.lastActiveAccount
                    "
                    @click="increaseBuyVolume(slotProps.data)"
                  >
                    V
                  </span>
                  <span
                    class="btn btn-light btn-sm mx-1"
                    v-if="
                      slotProps.data.ownerAddress !==
                      this.$store.state.wallet.lastActiveAccount
                    "
                    @click="hitAllBuy(slotProps.data)"
                  >
                    H
                  </span>
                  <span
                    class="btn btn-light btn-sm mx-1"
                    v-if="
                      this.order.quantity &&
                      slotProps.data.ownerAddress !==
                        this.$store.state.wallet.lastActiveAccount
                    "
                    @click="hitBuyPartial(slotProps.data)"
                  >
                    PH
                  </span>
                </template>
              </Column>
              <Column field="formattedASAAmount" header="Amount">
                <template #body="slotProps">
                  <div
                    class="text-end"
                    @click="
                      this.order.quantity = slotProps.data.formattedASAAmount;
                      this.processingOrder = false;
                    "
                  >
                    {{ slotProps.data.formattedASAAmount }}
                  </div>
                </template>
              </Column>
              <Column field="formattedPrice" header="Price">
                <template #body="slotProps">
                  <div
                    class="text-end"
                    @click="
                      this.order.price = slotProps.data.formattedPrice;
                      this.processingOrder = false;
                    "
                  >
                    {{ slotProps.data.formattedPrice }}
                  </div>
                </template></Column
              >
            </DataTable>
          </div>
          <div class="col">
            <h4>Offers</h4>
            <DataTable
              v-if="orders && orders.sellASAOrdersInEscrow"
              :value="orders.sellASAOrdersInEscrow"
              class="p-datatable-sm"
              responsiveLayout="scroll"
              :paginator="true"
              :rows="5"
              sort-field="asaPrice"
              :sort-order="1"
              stripedRows
              :rowClass="myOrderRow"
            >
              <Column field="formattedPrice" header="Price"
                ><template #body="slotProps">
                  <div
                    class="text-end"
                    @click="
                      this.order.price = slotProps.data.formattedPrice;
                      this.processingOrder = false;
                    "
                  >
                    {{ slotProps.data.formattedPrice }}
                  </div>
                </template></Column
              >
              <Column field="formattedASAAmount" header="Amount"
                ><template #body="slotProps">
                  <div
                    class="text-end"
                    @click="
                      this.order.quantity = slotProps.data.formattedASAAmount;
                      this.processingOrder = false;
                    "
                  >
                    {{ slotProps.data.formattedASAAmount }}
                  </div>
                </template></Column
              >
              <Column field="formattedPrice" header="">
                <template #body="slotProps">
                  <div class="text-end">
                    <span
                      class="btn btn-light btn-sm mx-1"
                      v-if="
                        this.order.quantity &&
                        slotProps.data.ownerAddress ===
                          this.$store.state.wallet.lastActiveAccount
                      "
                      @click="increaseSellVolume(slotProps.data)"
                    >
                      V
                    </span>
                    <span
                      class="btn btn-light btn-sm mx-1"
                      v-if="
                        slotProps.data.ownerAddress ===
                        this.$store.state.wallet.lastActiveAccount
                      "
                      @click="cancelSell(slotProps.data)"
                    >
                      X
                    </span>
                    <span
                      class="btn btn-light btn-sm mx-1"
                      v-if="
                        this.order.quantity &&
                        slotProps.data.ownerAddress !==
                          this.$store.state.wallet.lastActiveAccount
                      "
                      @click="hitSellPartial(slotProps.data)"
                    >
                      PH
                    </span>
                    <span
                      class="btn btn-light btn-sm mx-1"
                      v-if="
                        slotProps.data.ownerAddress !==
                        this.$store.state.wallet.lastActiveAccount
                      "
                      @click="hitAllSell(slotProps.data)"
                    >
                      H
                    </span>
                    <span>
                      <a
                        class="curier"
                        target="_blank"
                        :href="`https://testnet.algoexplorer.io/address/${slotProps.data.ownerAddress}`"
                        >{{ slotProps.data.ownerAddress.substring(0, 2) }}
                      </a>
                    </span>
                  </div>
                </template>
              </Column>
            </DataTable>
          </div>
        </div>
      </div>
      <div class="col">
        <Galleria :value="project.images" :numVisible="5">
          <template #item="slotProps">
            <img
              :src="slotProps.item.itemImageSrc"
              :alt="slotProps.item.alt"
              style="width: 100%"
            />
          </template>
          <template #thumbnail="slotProps">
            <img
              :src="slotProps.item.thumbnailImageSrc"
              :alt="slotProps.item.alt"
            />
          </template>

          <template #caption="{ item }">
            <h4 style="margin-bottom: 0.5rem">{{ item.title }}</h4>
            <p>{{ item.alt }}</p>
          </template>
        </Galleria>
      </div>
      <div class="col d-md-none d-lg-block">
        <LMap
          style="min-height: 50vh"
          :zoom="zoom"
          :min-zoom="minZoom"
          :max-zoom="maxZoom"
          :center="center"
        >
          <LTileLayer
            :url="url"
            :attribution="attribution"
            :options="options"
          />
          <LLayerGroup>
            <LMarker :lat-lng="getLatLng(project)">
              <LIcon :icon-url="getIcon(project)" />
            </LMarker>
          </LLayerGroup>
        </LMap>
      </div>
    </div>
  </PublicLayout>
</template>

<script>
// DON'T load Leaflet components here!
// Its CSS is needed though, if not imported elsewhere in your application.
import { mapActions } from "vuex";
import "leaflet/dist/leaflet.css";
import {
  LMap,
  LTileLayer,
  LMarker,
  LLayerGroup,
  LIcon,
} from "@vue-leaflet/vue-leaflet";

import PublicLayout from "../../layouts/Public.vue";
export default {
  components: {
    PublicLayout,
    LMap,
    LTileLayer,
    LMarker,
    LLayerGroup,
    LIcon,
  },
  watch: {
    selectedBid() {
      this.order.price = this.selectedBid.asaPrice;
    },
    selectedOffer() {
      this.order.price = this.selectedOffer.asaPrice;
    },
    account() {
      this.refreshAccountInfo();
    },
  },
  computed: {
    account() {
      return this.$store.state.wallet.privateAccounts.find(
        (a) => a.addr == this.$store.state.wallet.lastActiveAccount
      );
    },
    orderAlgoAmount() {
      if (!this.asset) return false;
      if (this.asset.decimals === undefined) return false;
      if (!this.order.price) return false;
      if (!this.order.quantity) return false;
      return (
        this.order.price *
        this.order.quantity *
        Math.pow(10, this.asset.decimals)
      );
    },
  },
  async mounted() {
    this.prolong();
    if (!this.timer) {
      this.timer = setInterval(this.countdown, 10000);
    } else {
      console.log("timer already running");
    }
    this.countdown();
    this.asset = await this.getAsset({ assetIndex: this.project.asa });
    await this.refreshAccountInfo();
  },
  beforeUnmount() {
    clearInterval(this.timer);
  },
  data() {
    return {
      info: {},
      assets: [],
      asset: {},
      processingOrder: false,
      orderstate: "",
      selectedBid: null,
      selectedOffer: null,
      timer: null,
      downloading: false,
      orders: { buyASAOrdersInEscrow: [], sellASAOrdersInEscrow: [] },
      order: {
        isOffer: false,
        quantity: null,
        price: null,
      },
      project: {
        id: "2",
        address: "Prague",
        top: true,
        asa: 15322902, //37074699, //21582668, 33698417,15322902,39247510,22847688,21582668
        lat: 47.369450301672266,
        lng: 8.539875999999893,
        name: "Trust Square",
        rate: 0.0415,
        image:
          "https://d18-a.sdn.cz/d_18/c_img_QJ_JV/ZGHIsk.jpeg?fl=res,749,562,3|shr,,20|jpg,90",
        images: [
          {
            itemImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria1.jpg",
            thumbnailImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria1s.jpg",
            alt: "Description for Image 1",
            title: "Title 1",
          },
          {
            itemImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria2.jpg",
            thumbnailImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria2s.jpg",
            alt: "Description for Image 2",
            title: "Title 2",
          },
          {
            itemImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria3.jpg",
            thumbnailImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria3s.jpg",
            alt: "Description for Image 3",
            title: "Title 3",
          },
          {
            itemImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria4.jpg",
            thumbnailImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria4s.jpg",
            alt: "Description for Image 4",
            title: "Title 4",
          },
          {
            itemImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria5.jpg",
            thumbnailImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria5s.jpg",
            alt: "Description for Image 5",
            title: "Title 5",
          },
          {
            itemImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria6.jpg",
            thumbnailImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria6s.jpg",
            alt: "Description for Image 6",
            title: "Title 6",
          },
          {
            itemImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria7.jpg",
            thumbnailImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria7s.jpg",
            alt: "Description for Image 7",
            title: "Title 7",
          },
          {
            itemImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria8.jpg",
            thumbnailImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria8s.jpg",
            alt: "Description for Image 8",
            title: "Title 8",
          },
          {
            itemImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria9.jpg",
            thumbnailImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria9s.jpg",
            alt: "Description for Image 9",
            title: "Title 9",
          },
          {
            itemImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria10.jpg",
            thumbnailImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria10s.jpg",
            alt: "Description for Image 10",
            title: "Title 10",
          },
          {
            itemImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria11.jpg",
            thumbnailImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria11s.jpg",
            alt: "Description for Image 11",
            title: "Title 11",
          },
          {
            itemImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria12.jpg",
            thumbnailImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria12s.jpg",
            alt: "Description for Image 12",
            title: "Title 12",
          },
          {
            itemImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria13.jpg",
            thumbnailImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria13s.jpg",
            alt: "Description for Image 13",
            title: "Title 13",
          },
          {
            itemImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria14.jpg",
            thumbnailImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria14s.jpg",
            alt: "Description for Image 14",
            title: "Title 14",
          },
          {
            itemImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria15.jpg",
            thumbnailImageSrc:
              "https://primefaces.org/primevue/showcase/demo/images/galleria/galleria15s.jpg",
            alt: "Description for Image 15",
            title: "Title 15",
          },
        ],
      },
      mapIsReady: false,
      minZoom: 3,
      maxZoom: 18,
      zoom: 15,
      center: [47.369450301672266, 8.539875999999893],
      url: "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
      attribution:
        '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors',
      options: {
        useCache: true,
        crossOrigin: true,
      },
    };
  },
  async beforeMount() {
    this.mapIsReady = true;
  },
  methods: {
    ...mapActions({
      axiosGet: "axios/get",
      algodexBuy: "algodex/algodexBuy",
      algodexCancelBuy: "algodex/cancelBuy",
      algodexHitAllBuy: "algodex/hitAllBuy",
      algodexHitBuyPartial: "algodex/hitBuyPartial",
      algodexIncreaseBuyVolume: "algodex/increaseBuyVolume",
      algodexSell: "algodex/algodexSell",
      algodexHitAllSell: "algodex/hitAllSell",
      algodexCancelSell: "algodex/cancelSell",
      algodexHitSellPartial: "algodex/hitSellPartial",
      algodexIncreaseSellVolume: "algodex/increaseSellVolume",
      waitForConfirmation: "algod/waitForConfirmation",
      accountInformation: "algod/accountInformation",
      getAsset: "indexer/getAsset",
      prolong: "wallet/prolong",
      openError: "toast/openError",
    }),
    getAssetSync(id) {
      const ret = this.$store.state.indexer.assets.find(
        (a) => a["asset-id"] == id
      );
      return ret;
    },
    getAssetName(id) {
      const asset = this.getAssetSync(id);
      if (asset) return asset["name"];
    },
    getAccountAssetBalance() {
      if (!this.info) return;
      if (!this.project.asa) return;
      if (!this.info.assets) return;

      const find = this.info.assets.find(
        (x) => x["asset-id"] == this.project.asa
      );
      if (!find) return;
      return find["amount"];
    },
    getAssetDecimals(id) {
      const asset = this.getAssetSync(id);
      if (asset) return asset["decimals"];
    },
    async refreshAccountInfo() {
      if (!this.account) {
        this.info = {};
        return;
      }
      this.info = await this.accountInformation({
        addr: this.account.addr,
      });
    },
    bidClick(e) {
      if (e.index === 1) {
        this.order.price = e.data.asaPrice;
      }
      if (e.index === 0) {
        this.order.quantity = e.data.asaAmount / Math.pow(10, e.data.decimals);
      }
    },
    offerClick(e) {
      if (e.index == 0) {
        this.order.price = e.data.asaPrice;
      }
      if (e.index == 1) {
        this.order.quantity = e.data.asaAmount / Math.pow(10, e.data.decimals);
      }
    },

    async countdown() {
      if (this.downloading) return;
      this.downloading = true;
      try {
        const ord = await this.axiosGet({
          url: `https://api-testnet-public.algodex.com/algodex-backend/orders.php?assetId=${this.project.asa}`,
        });
        if (ord) {
          this.orders = ord;
        }
      } catch (e) {
        console.log("error downloading", e);
      }
      this.downloading = false;
    },
    getLatLng(branch) {
      if (!branch) return { lat: 0, lng: 0 };
      return { lat: branch.lat, lng: branch.lng };
    },
    getIcon(place) {
      if (place.top) {
        return "https://cdnsitestoragecovid.blob.core.windows.net/web/images/icons/map_icon_orange.png";
      } else if (place.asa) {
        return "https://cdnsitestoragecovid.blob.core.windows.net/web/images/icons/map_icon_gray_01.png";
      } else {
        return "https://cdnsitestoragecovid.blob.core.windows.net/web/images/icons/map_icon_blue.png";
      }
    },
    async makerSell() {
      this.prolong();
      this.processingOrder = true;
      this.orderstate = "Sending to net";
      const tx = await this.algodexSell({
        creator: this.account,
        price: Math.round(this.order.price * 1000000),
        assetIndex: this.project.asa,
        amount: Math.round(this.order.quantity * 1000000),
      });
      if (tx) {
        this.orderstate = "Sent to net";
        const confirmation = await this.waitForConfirmation({
          txId: tx.txId,
          timeout: 5,
        });
        if (confirmation) {
          this.orderstate = "Confirmed block";
        }
      } else {
        this.orderstate = "Error";
      }
      //this.processingOrder = false;
    },
    async makerBuy() {
      this.prolong();
      this.processingOrder = true;
      this.orderstate = "Sending to net";
      const tx = await this.algodexBuy({
        creator: this.account,
        price: Math.round(this.order.price * 1000000),
        assetIndex: this.project.asa,
        amount: Math.round(this.order.quantity * 1000000),
      });
      if (tx && tx.error) {
        this.openError(tx.error);
        return;
      }
      if (tx) {
        this.orderstate = "Sent to net";
        const confirmation = await this.waitForConfirmation({
          txId: tx.txId,
          timeout: 5,
        });
        if (confirmation) {
          this.orderstate = "Confirmed block";
        }
      } else {
        this.orderstate = "Error";
      }

      //this.processingOrder = false;
    },
    myOrderRow(data) {
      if (this.$store && this.$store.state && this.$store.state.wallet) {
        if (data.ownerAddress === this.$store.state.wallet.lastActiveAccount)
          return "bg-warning";
      }
      return null;
    },
    async cancelBuy(data) {
      this.prolong();
      this.processingOrder = true;
      this.orderstate = "Sending C to net";
      const tx = this.algodexCancelBuy({
        ownerAddress: data.ownerAddress,
        escrowAddress: data.escrowAddress,
        appIndex: data.appId,
        assetIndex: data.assetId,
      });
      if (tx && tx.error) {
        this.openError(tx.error);
        return;
      }
      if (tx) {
        this.orderstate = "Sent to net";
        const confirmation = await this.waitForConfirmation({
          txId: tx.txId,
          timeout: 5,
        });
        if (confirmation) {
          this.orderstate = "Confirmed block";
        }
      } else {
        this.orderstate = "Error";
      }
    },
    async hitAllBuy(data) {
      this.prolong();
      this.processingOrder = true;
      this.orderstate = "Sending H to net";
      const algoAmount = data.algoAmount;
      const assetAmount = Math.round(algoAmount / data.asaPrice);
      //let stop = true;
      //if (stop) return;
      const tx = await this.algodexHitAllBuy({
        ownerAddress: data.ownerAddress,
        newOwnerAddress: this.$store.state.wallet.lastActiveAccount,
        algoAmount,
        assetAmount,
        escrowAddress: data.escrowAddress,
        appIndex: data.appId,
        assetIndex: data.assetId,
      });
      if (tx && tx.error) {
        this.openError(tx.error);
        return;
      }
      if (tx) {
        this.orderstate = "Sent to net";
        const confirmation = await this.waitForConfirmation({
          txId: tx.txId,
          timeout: 5,
        });
        if (confirmation) {
          this.orderstate = "Confirmed block";
        }
      } else {
        this.orderstate = "Error";
      }
    },
    async hitBuyPartial(data) {
      this.prolong();
      this.processingOrder = true;
      this.orderstate = "Sending H to net";
      const asaPrice = data.asaPrice;
      const newQ = Math.ceil(this.order.quantity * 1000000);
      const tx = await this.algodexHitBuyPartial({
        ownerAddress: data.ownerAddress,
        newOwnerAddress: this.$store.state.wallet.lastActiveAccount,
        asaPrice,
        assetAmount: newQ,
        escrowAddress: data.escrowAddress,
        appIndex: data.appId,
        assetIndex: data.assetId,
      });
      if (tx && tx.error) {
        this.openError(tx.error);
        return;
      }
      if (tx) {
        this.orderstate = "Sent to net";
        const confirmation = await this.waitForConfirmation({
          txId: tx.txId,
          timeout: 5,
        });
        if (confirmation) {
          this.orderstate = "Confirmed block";
        }
      } else {
        this.orderstate = "Error";
      }
    },
    async increaseBuyVolume(data) {
      this.prolong();
      this.processingOrder = true;
      this.orderstate = "Sending H to net";
      const asaPrice = data.asaPrice;
      const newQ = Math.ceil(asaPrice * this.order.quantity * 1000000);
      const tx = await this.algodexIncreaseBuyVolume({
        ownerAddress: this.$store.state.wallet.lastActiveAccount,
        algoAmount: newQ,
        escrowAddress: data.escrowAddress,
        appIndex: data.appId,
        assetIndex: data.assetId,
      });
      if (tx && tx.error) {
        this.openError(tx.error);
        return;
      }
      if (tx) {
        this.orderstate = "Sent to net";
        const confirmation = await this.waitForConfirmation({
          txId: tx.txId,
          timeout: 5,
        });
        if (confirmation) {
          this.orderstate = "Confirmed block";
        }
      } else {
        this.orderstate = "Error";
      }
    },
    async cancelSell(data) {
      this.prolong();
      this.processingOrder = true;
      this.orderstate = "Sending C to net";
      const tx = this.algodexCancelSell({
        ownerAddress: data.ownerAddress,
        escrowAddress: data.escrowAddress,
        appIndex: data.appId,
        assetIndex: data.assetId,
      });
      if (tx) {
        this.orderstate = "Sent to net";
        const confirmation = await this.waitForConfirmation({
          txId: tx.txId,
          timeout: 5,
        });
        if (confirmation) {
          this.orderstate = "Confirmed block";
        }
      } else {
        this.orderstate = "Error";
      }
    },
    async hitAllSell(data) {
      this.prolong();
      this.processingOrder = true;
      this.orderstate = "Sending H to net";
      const assetAmount = data.asaAmount;
      const algoAmount = Math.ceil(assetAmount * data.asaPrice);
      const tx = await this.algodexHitAllSell({
        ownerAddress: data.ownerAddress,
        newOwnerAddress: this.$store.state.wallet.lastActiveAccount,
        algoAmount,
        assetAmount,
        escrowAddress: data.escrowAddress,
        appIndex: data.appId,
        assetIndex: data.assetId,
      });
      if (tx && tx.error) {
        this.openError(tx.error);
        return;
      }
      if (tx) {
        this.orderstate = "Sent to net";
        const confirmation = await this.waitForConfirmation({
          txId: tx.txId,
          timeout: 5,
        });
        if (confirmation) {
          this.orderstate = "Confirmed block";
        }
      } else {
        this.orderstate = "Error";
      }
    },
    async hitSellPartial(data) {
      this.prolong();
      this.processingOrder = true;
      this.orderstate = "Sending H to net";
      const asaPrice = data.asaPrice;
      const newQ = Math.ceil(this.order.quantity * 1000000);
      const tx = await this.algodexHitSellPartial({
        ownerAddress: data.ownerAddress,
        newOwnerAddress: this.$store.state.wallet.lastActiveAccount,
        asaPrice,
        assetAmount: newQ,
        escrowAddress: data.escrowAddress,
        appIndex: data.appId,
        assetIndex: data.assetId,
      });
      if (tx && tx.error) {
        this.openError(tx.error);
        return;
      }
      if (tx) {
        this.orderstate = "Sent to net";
        const confirmation = await this.waitForConfirmation({
          txId: tx.txId,
          timeout: 5,
        });
        if (confirmation) {
          this.orderstate = "Confirmed block";
        }
      } else {
        this.orderstate = "Error";
      }
    },
    async increaseSellVolume(data) {
      this.prolong();
      this.processingOrder = true;
      this.orderstate = "Sending H to net";
      const newQ = Math.ceil(this.order.quantity * 1000000);
      const tx = await this.algodexIncreaseSellVolume({
        ownerAddress: this.$store.state.wallet.lastActiveAccount,
        asaAmount: newQ,
        escrowAddress: data.escrowAddress,
        appIndex: data.appId,
        assetIndex: data.assetId,
      });
      if (tx && tx.error) {
        this.openError(tx.error);
        return;
      }
      if (tx) {
        this.orderstate = "Sent to net";
        const confirmation = await this.waitForConfirmation({
          txId: tx.txId,
          timeout: 5,
        });
        if (confirmation) {
          this.orderstate = "Confirmed block";
        }
      } else {
        this.orderstate = "Error";
      }
    },
  },
};
</script>
<style scoped>
.curier {
  font-family: "Courier New", Courier, monospace;
}
</style>