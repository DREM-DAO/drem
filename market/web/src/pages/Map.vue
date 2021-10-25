<template>
  <PublicLayout>
    <div class="row my-4">
      <div class="col">
        <MultiSelect
          v-model="filterPropertyType"
          :options="propertyTypeOptions"
          optionLabel="name"
          placeholder="Select property type"
          class="w-100"
        />
      </div>
      <div class="col">
        <MultiSelect
          v-model="filterInvestmentType"
          :options="investmentTypeOptions"
          optionLabel="name"
          placeholder="Select investment type"
          class="w-100"
        />
      </div>
      <div class="col">
        <MultiSelect
          v-model="filterCountry"
          :options="countryOptions"
          optionLabel="name"
          placeholder="Select country"
          class="w-100"
        />
      </div>
      <div class="col">
        <MultiSelect
          v-model="filterCurrency"
          :options="currencyOptions"
          optionLabel="name"
          placeholder="Select currency"
          class="w-100"
        />
      </div>
      <div class="col">
        <MultiSelect
          v-model="filterCity"
          :options="cityOptions"
          optionLabel="name"
          placeholder="Select city"
          class="w-100"
        />
      </div>
    </div>
    <div class="row">
      <div class="col">
        <div
          class="
            row
            row-cols-xxl-4
            row-cols-lg-4
            row-cols-md-2
            row-cols-sm-1
            row-cols-1
          "
        >
          <div class="col" v-for="place in topplaces" :key="place.id">
            <div
              class="card mr-3 mb-3 cardlink"
              @click="$router.push(`/project/${place.id}`)"
            >
              <img
                :src="place.image"
                class="card-img"
                :alt="place.name"
                height="200"
                style="object-fit: cover"
              />

              <div class="card-img-overlay d-flex flex-column p-0">
                <div class="c-title d-flex justify-content-between">
                  <div class="p-1">
                    {{ place.name }}
                  </div>
                  <span class="p-1">
                    <img
                      :alt="place.countryName"
                      :src="'/flags/3x2/' + place.country + '.svg'"
                      height="25"
                      class="rounded"
                    />
                  </span>
                </div>
                <div class="mt-auto c-subtitle d-flex justify-content-between">
                  <span class="">
                    IRR {{ $filters.formatPercent(place.rate, 4) }}
                  </span>
                  <span class="text-right">
                    Buy at
                    {{ $filters.formatCurrency(220000, place.currencyName, 0) }}
                  </span>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="row row-cols-lg-2 row-cols-md-2 row-cols-sm-1 row-cols-1">
          <div class="col">
            <div
              class="card mr-3 mb-3 cardlink"
              @click="$router.push(`/recurring/`)"
            >
              <img
                src="https://d18-a.sdn.cz/d_18/c_img_gY_e/HSeSpG.jpeg?fl=res,749,562,3|shr,,20|jpg,90"
                class="card-img"
                height="200"
                style="object-fit: cover"
              />

              <div class="card-img-overlay d-flex flex-column p-0">
                <div class="c-title p-1">Recurring investment</div>
                <div class="mt-auto c-subtitle p-1">
                  Buy best investment opportunity
                </div>
              </div>
            </div>
          </div>
          <div class="col">
            <div
              class="row row-cols-lg-2 row-cols-md-2 row-cols-sm-1 row-cols-1"
            >
              <div class="col" v-for="place in hotplaces" :key="place.id">
                <div
                  class="card mr-3 mb-3 cardlink"
                  @click="$router.push(`/project/${place.id}`)"
                >
                  <img
                    :src="place.image"
                    class="card-img"
                    :alt="place.name"
                    height="200"
                    style="object-fit: cover"
                  />

                  <div class="card-img-overlay d-flex flex-column p-0">
                    <div class="c-title p-1">
                      {{ place.name }}
                    </div>
                    <div class="mt-auto c-subtitle p-1">
                      Hot deal IRR {{ $filters.formatPercent(place.rate, 4) }}
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-4 d-none d-xxl-block">
        <div class="card">
          <LMap
            style="height: 410px"
            class="m-0 p-0"
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
            <LLayerGroup v-if="places">
              <LMarker
                v-for="place in places"
                :key="place.id"
                :lat-lng="getLatLng(place)"
              >
                <LIcon :icon-url="getIcon(place)" />
                <LPopup :options="{ autoClose: true, closeOnClick: false }">
                  <h4
                    class="md-auto"
                    style="text-align: center; min-width: 150px"
                  >
                    {{ place.name }}
                  </h4>
                  <VLink :href="`/project/${place.id}`">
                    <img
                      class="img thumbnail"
                      :src="place.image"
                      style="max-width: 150px"
                    />
                  </VLink>
                  <p>Rate: {{ $filters.formatPercent(place.rate, 4) }}</p>

                  <p v-if="place.asa">
                    <a
                      target="_blank"
                      rel="norefferer"
                      :href="`https://testnet.algoexplorer.io/asset/${place.asa}`"
                      >AlgoExplorer info</a
                    >
                  </p>
                </LPopup>
              </LMarker>
            </LLayerGroup>
          </LMap>
        </div>
      </div>
    </div>
    <div class="row row-cols-lg-6 row-cols-md-4 row-cols-sm-2 row-cols-xs-1">
      <div class="col" v-for="place in filteredItems" :key="place.id">
        <div
          class="card mr-3 mb-3 cardlink"
          @click="$router.push(`/project/${place.id}`)"
        >
          <img
            :src="place.image"
            class="card-img"
            :alt="place.name"
            height="200"
            style="object-fit: cover"
          />

          <div class="card-img-overlay d-flex flex-column p-0">
            <div class="c-title p-1">
              {{ place.name }}
            </div>
            <div class="mt-auto c-subtitle d-flex justify-content-between">
              <span class="" v-if="place.time">
                {{ place.time }}
              </span>
              <span v-else>
                IRR {{ $filters.formatPercent(place.rate, 4) }}
              </span>
              <span class="text-right">
                {{ $filters.formatCurrency(220000, place.currencyName, 0) }}
              </span>
            </div>
          </div>
        </div>
      </div>
    </div>
  </PublicLayout>
</template>

<script>
// DON'T load Leaflet components here!
// Its CSS is needed though, if not imported elsewhere in your application.
import "leaflet/dist/leaflet.css";
import {
  LMap,
  LTileLayer,
  LPopup,
  LMarker,
  LLayerGroup,
  LIcon,
} from "@vue-leaflet/vue-leaflet";
import moment from "moment";
import momentDurationFormatSetup from "moment-duration-format";
import uniqWith from "lodash/uniqWith";
import isEqual from "lodash/isEqual";

momentDurationFormatSetup(moment);

import PublicLayout from "../layouts/Public.vue";
import VLink from "../components/VLink.vue";
export default {
  components: {
    PublicLayout,
    LMap,
    LTileLayer,
    LPopup,
    LMarker,
    LLayerGroup,
    LIcon,
    VLink,
  },
  data() {
    return {
      filterPropertyType: [],
      filterCountry: [],
      filterCity: [],
      filterInvestmentType: [],
      filterCurrency: [],

      propertyTypeOptions: [
        { name: "House", value: "house" },
        { name: "Appartment", value: "appartment" },
        { name: "Hotel", value: "hotel" },
        { name: "Commercial", value: "commercial" },
        { name: "Aggriculture", value: "aggriculture" },
        { name: "Garage", value: "garage" },
      ],
      investmentTypeOptions: [
        { name: "Initial project offering", value: "ico" },
        { name: "Live real estate project", value: "live" },
        { name: "Bond investment", value: "bond" },
      ],
      countryOptions: [
        { name: "USA", value: "usa" },
        { name: "EU", value: "eu" },
        { name: "Russia", value: "russia" },
        { name: "Australia", value: "australia" },
      ],
      cityOptions: [
        { name: "Texas", value: "tx" },
        { name: "Prague", value: "prg" },
        { name: "Zurrich", value: "zurrich" },
      ],
      places: [
        {
          id: "1",
          asa: 37074699,
          top: false,
          lat: 50,
          lng: 20,
          name: "AA",
          rate: 0.0405,
          country: "sk",
          countryName: "Slovakia",
          image:
            "https://d18-a.sdn.cz/d_18/c_img_gW_f/wZ1NHI.jpeg?fl=res,749,562,3|shr,,20|jpg,90",
          currency: "eur",
          currencyName: "EUR",
        },
        {
          id: "2",
          top: true,
          asa: 33698417,
          lat: 47.369450301672266,
          lng: 8.539875999999893,
          name: "Trust Square",
          countdown: "2021-10-31T12:00:00+01:00",
          rate: 0.0415,
          country: "ch",
          countryName: "Switzerland",
          image:
            "https://d18-a.sdn.cz/d_18/c_img_QJ_JV/ZGHIsk.jpeg?fl=res,749,562,3|shr,,20|jpg,90",
          currency: "eur",
          currencyName: "EUR",
        },
        {
          id: "3",
          top: false,
          lat: 50,
          lng: -71,
          name: "Canada Wood",
          rate: 0.0205,
          country: "ca",
          countryName: "Canada",
          image:
            "https://d18-a.sdn.cz/d_18/c_img_gV_a/eJcsgF.jpeg?fl=res,749,562,3|shr,,20|jpg,90",
          currency: "eur",
          currencyName: "EUR",
        },
        {
          id: "3",
          top: false,
          lat: 50,
          lng: -71,
          name: "Canada Wood",
          rate: 0.0205,
          country: "ch",
          countryName: "Switzerland",
          image:
            "https://d18-a.sdn.cz/d_18/c_img_gV_a/eJcsgF.jpeg?fl=res,749,562,3|shr,,20|jpg,90",
          currency: "usd",
          currencyName: "USD",
        },
        {
          id: "3",
          top: false,
          lat: 50,
          lng: -71,
          name: "Canada Wood",
          rate: 0.0205,
          country: "ch",
          countryName: "Switzerland",
          image:
            "https://d18-a.sdn.cz/d_18/c_img_gV_a/eJcsgF.jpeg?fl=res,749,562,3|shr,,20|jpg,90",
          currency: "usd",
          currencyName: "USD",
        },
        {
          id: "3",
          top: false,
          lat: 50,
          lng: -71,
          name: "Canada Wood",
          rate: 0.0205,
          country: "ch",
          countryName: "Switzerland",
          image:
            "https://d18-a.sdn.cz/d_18/c_img_gV_a/eJcsgF.jpeg?fl=res,749,562,3|shr,,20|jpg,90",
          currency: "usd",
          currencyName: "USD",
        },
        {
          id: "3",
          top: false,
          lat: 50,
          lng: -71,
          name: "Canada Wood",
          rate: 0.0205,
          image:
            "https://d18-a.sdn.cz/d_18/c_img_gV_a/eJcsgF.jpeg?fl=res,749,562,3|shr,,20|jpg,90",
          currency: "usd",
          currencyName: "USD",
        },
        {
          id: "3",
          top: false,
          lat: 50,
          lng: -71,
          name: "Canada Wood",
          rate: 0.0205,
          image:
            "https://d18-a.sdn.cz/d_18/c_img_gV_a/eJcsgF.jpeg?fl=res,749,562,3|shr,,20|jpg,90",
          currency: "usd",
          currencyName: "USD",
        },
        {
          id: "3",
          top: false,
          lat: 50,
          lng: -71,
          name: "Canada Wood",
          rate: 0.0205,
          image:
            "https://d18-a.sdn.cz/d_18/c_img_gV_a/eJcsgF.jpeg?fl=res,749,562,3|shr,,20|jpg,90",
          currency: "usd",
          currencyName: "USD",
        },
      ],
      mapIsReady: false,
      minZoom: 2,
      maxZoom: 18,
      zoom: 2,
      center: [50, 0],
      url: "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
      attribution:
        '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors',
      options: {
        useCache: true,
        crossOrigin: true,
      },
    };
  },
  computed: {
    filteredItems() {
      let ret = this.places;
      if (Object.values(this.filterCurrency).length > 0) {
        const currencies = this.filterCurrency.map((c) => c.value);
        console.log("filterCurrency", currencies, this.filterCurrency);
        ret = ret.filter((i) => currencies.indexOf(i.currency) >= 0);
      }
      return ret;
    },
    currencyOptions() {
      const currencies = this.places.map((i) => ({
        name: i.currencyName,
        value: i.currency,
      }));
      const uniqCurrencies = uniqWith(currencies, isEqual);
      console.log("currencies", currencies, uniqCurrencies);
      return uniqCurrencies;
    },
    topplaces() {
      return [this.places[0], this.places[1], this.places[2], this.places[3]];
    },
    hotplaces() {
      return [this.places[0], this.places[1]];
    },
  },

  async beforeMount() {
    this.mapIsReady = true;
  },
  created() {
    console.log("mmounted", this.places);
    if (!this.timer) {
      this.timer = setInterval(this.countdown, 1000);
    } else {
      console.log("timer already running");
    }
    this.countdown();
  },
  beforeUnmount() {
    clearInterval(this.timer);
  },
  methods: {
    async countdown() {
      for (let index in this.places) {
        const place = this.places[index];
        if (place.countdown) {
          const now = moment();
          const end = moment(place.countdown);
          var duration = moment.duration(end.diff(now));
          place.time = duration.format("HH:mm:ss");
        }
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
  },
};
</script>
<style scoped>
.c-title {
  color: #eee;
  background: rgba(0, 0, 0, 0.2);
  text-shadow: 1px 1px 1px #000;
  font-size: 1.8em;
  font-weight: bold;
  text-decoration: none;
}

.c-subtitle {
  color: #eee;
  text-shadow: 1px 1px 1px #111;
  background: rgba(0, 0, 0, 0.3);
  font-size: 0.8em;
  font-weight: bold;
  text-decoration: none;
  padding: 0 1ex;
}
.cardlink {
  border: 1px solid #999;
}
.cardlink:hover {
  transform: scale(1.05);
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.12), 0 4px 8px rgba(0, 0, 0, 0.06);
  cursor: pointer;
}
.cardlink:hover {
  -webkit-transition: opacity 0.2s ease-in-out;
  -moz-transition: opacity 0.2s ease-in-out;
  -ms-transition: opacity 0.2s ease-in-out;
  -o-transition: opacity 0.2s ease-in-out;
  transition: opacity 0.2s ease-in-out;
}
.card-img-overlay {
  opacity: 1;
}
.card-img-overlay:hover {
  background: linear-gradient(
    rgba(0, 0, 0, 0.5),
    rgba(0, 0, 0, 0.01),
    rgba(0, 0, 0, 0.01),
    rgba(0, 0, 0, 0.5)
  );
}
</style>