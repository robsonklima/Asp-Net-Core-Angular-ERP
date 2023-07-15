export function emailValidator(email) {
  const re = /\S+@\S+\.\S+/
  if (!email) return "Email não pode ser vazio."
  if (!re.test(email)) return 'Ooops! Informe um e-mail válido.'
  return ''
}
