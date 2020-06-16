<template>
  <div>
    <div class="members-container bg">
      <equipe-item :items="creators" title="Idealizadores Aus Ouvidos" />
    </div>
    <div class="members-container">
      <equipe-item :items="supporters" title="Apoiadores Aus Ouvidos" />
    </div>
    <div class="members-container bg">
      <equipe-item :items="teamMembers" title="Equipe Aus Ouvidos" />
    </div>
  </div>
</template>

<script lang="ts" src="./equipe.ts"></script>

