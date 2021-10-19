<template>
  <PublicLayout>
    <div class="alert alert-warning">Project is in the TestNet phase</div>
    <div class="row">
      <div class="col-6">
        <div class="row row-cols-lg-3 row-cols-md-2 row-cols-sm-1">
          <div class="col" v-for="place in places" :key="place.id">
            <div
              class="card mr-3 mb-3"
              @click="$router.push(`/project/${place.id}`)"
            >
              <img
                :src="place.image"
                class="card-img"
                :alt="place.name"
                height="200"
                style="object-fit: cover"
              />

              <div class="card-img-overlay d-flex flex-column">
                <div class="c-title">
                  {{ place.name }}
                </div>
                <div class="mt-auto c-subtitle">
                  IRR {{ $filters.formatPercent(place.rate, 4) }}
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-8">
            <div class="card mr-3 mb-3" @click="$router.push(`/recurring/`)">
              <img
                src="https://d18-a.sdn.cz/d_18/c_img_gY_e/HSeSpG.jpeg?fl=res,749,562,3|shr,,20|jpg,90"
                class="card-img"
                height="200"
                style="object-fit: cover"
              />

              <div class="card-img-overlay d-flex flex-column">
                <div class="c-title">Recurring investment</div>
                <div class="mt-auto c-subtitle">
                  Buy best investment opportunity
                </div>
              </div>
            </div>
          </div>
          <div class="col-4" v-for="place in hotplaces" :key="place.id">
            <div
              class="card mr-3 mb-3"
              @click="$router.push(`/project/${place.id}`)"
            >
              <img
                :src="place.image"
                class="card-img"
                :alt="place.name"
                height="200"
                style="object-fit: cover"
              />

              <div class="card-img-overlay d-flex flex-column">
                <div class="c-title">
                  {{ place.name }}
                </div>
                <div class="mt-auto c-subtitle">
                  Hot deal IRR {{ $filters.formatPercent(place.rate, 4) }}
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="row row-cols-lg-3 row-cols-md-2 row-cols-sm-1">
          <div class="col" v-for="place in places" :key="place.id">
            <div
              class="card mr-3 mb-3"
              @click="$router.push(`/project/${place.id}`)"
            >
              <img
                :src="place.image"
                class="card-img"
                :alt="place.name"
                height="200"
                style="object-fit: cover"
              />

              <div class="card-img-overlay d-flex flex-column">
                <div class="c-title">
                  {{ place.name }}
                </div>
                <div class="mt-auto c-subtitle">
                  IRR {{ $filters.formatPercent(place.rate, 4) }}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-6">
        <LMap
          style="min-height: 80vh"
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
              <LIcon :icon-url="getIcon(place)" :iconAnchor="[12, 12]" />
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
      places: [
        {
          id: "1",
          asa: 37074699,
          top: false,
          lat: 50,
          lng: 20,
          name: "AA",
          rate: 0.0405,
          image:
            "https://d18-a.sdn.cz/d_18/c_img_gW_f/wZ1NHI.jpeg?fl=res,749,562,3|shr,,20|jpg,90",
        },
        {
          id: "2",
          top: true,
          asa: 33698417,
          lat: 47.369450301672266,
          lng: 8.539875999999893,
          name: "Trust Square",
          rate: 0.0415,
          image:
            "https://d18-a.sdn.cz/d_18/c_img_QJ_JV/ZGHIsk.jpeg?fl=res,749,562,3|shr,,20|jpg,90",
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
        },
      ],
      mapIsReady: false,
      minZoom: 3,
      maxZoom: 18,
      zoom: 4,
      center: [30, 1],
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
    hotplaces() {
      return [this.places[0]];
    },
  },

  async beforeMount() {
    this.mapIsReady = true;
  },
  methods: {
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
  color: #fff !important;
  text-shadow: 1px 1px 1px #000;
  font-size: 1.8em;
  font-weight: bold;
  text-decoration: none;
}

.c-subtitle {
  color: #fff !important;
  text-shadow: 1px 1px 1px #000;
  font-size: 1.2em;
  font-weight: bold;
  text-decoration: none;
}

.card:hover {
  transform: scale(1.05);
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.12), 0 4px 8px rgba(0, 0, 0, 0.06);
  cursor: pointer;
}
.card:hover {
  -webkit-transition: opacity 0.2s ease-in-out;
  -moz-transition: opacity 0.2s ease-in-out;
  -ms-transition: opacity 0.2s ease-in-out;
  -o-transition: opacity 0.2s ease-in-out;
  transition: opacity 0.2s ease-in-out;
  opacity: 0.8;
}
.card-img-overlay {
  opacity: 1;
}
</style>