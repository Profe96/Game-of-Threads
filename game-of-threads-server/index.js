const express = require('express');
const googleAuth = require('./controllers/googleAuth');

const app = express();
const port = process.env.PORT || 8000;

app.get('/', (req, res) => {
  res.send({ 'url': googleAuth.urlGoogle() });
});

app.get('/juan', (req, res) => {
  console.log(req);
  res.send('ahuevo');
});

app.get('/auth/google/callback' , (req, res) => {
  console.log(req)
});

app.listen(port, () => console.log(`Listening on port ${port}`));