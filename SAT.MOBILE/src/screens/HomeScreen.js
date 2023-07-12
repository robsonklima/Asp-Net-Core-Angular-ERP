import React, { useEffect, useState } from "react";
import api from '../services/api'

export default function HomeScreen({ navigation }) {
  const [chamados, setChamados] = useState([]);

  useEffect(() => {
    api
      .get('OrdemServico', {
        params: {
          codTecnico: 1153,
          pageSize: 10
        }
      })
      .then((response) => {
        response.data.items.forEach(el => { setChamados(chamados.push(el)) });
      })
      .catch(e => {
        console.log(e);
      });
  }, []);

  return (
    <div>
      <h1>OLA</h1>
    </div>
  );
}