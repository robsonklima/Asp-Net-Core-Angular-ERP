import React from 'react'
import Background from '../components/Background'
import Logo from '../components/Logo'
import Header from '../components/Header'
import Paragraph from '../components/Paragraph'
import Button from '../components/Button'

export default function HomeScreen({ navigation }) {
  return (
    <Background>
      <Logo />
      <Header>Vamos iniciar</Header>
      <Paragraph>
        ...
      </Paragraph>
      <Button
        mode="outlined"
        onPress={() =>
          navigation.reset({
            index: 0,
            routes: [{ name: 'StartScreen' }],
          })
        }
      >
        Sair
      </Button>
    </Background>
  )
}
