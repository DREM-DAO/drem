<template>
  <PublicLayout>
    <h1>{{ project.name }}</h1>
    <img class="img thumbnail" :src="project.image" style="max-width: 150px" />
    <div class="row">
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
                <VLink :href="`/project/${place.id}`"> </VLink>
                <p>Rate: {{ $filters.formatPercent(place.rate, 4) }}</p></LPopup
              >
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

import VLink from "../../components/VLink.vue";
import PublicLayout from "../../layouts/Public.vue";
export default {
  components: {
    PublicLayout,
    VLink,
    LMap,
    LTileLayer,
    LPopup,
    LMarker,
    LLayerGroup,
    LIcon,
  },
  data() {
    return {
      project: {
        id: "2",
        top: true,
        asa: "11",
        lat: 47.369450301672266,
        lng: 8.539875999999893,
        name: "Trust Square",
        rate: 0.0415,
        image:
          "https://d18-a.sdn.cz/d_18/c_img_QJ_JV/ZGHIsk.jpeg?fl=res,749,562,3|shr,,20|jpg,90",
      },
      mapIsReady: false,
      minZoom: 15,
      maxZoom: 18,
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