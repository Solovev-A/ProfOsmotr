import styled from "styled-components"

export const ControlRow = styled.div`
display: flex;
`

export const ActionsContainer = styled.div`
margin-left: 3px;
display: grid;
grid-template-columns: repeat(2, 1fr);
grid-column-gap: 2px;
max-height: calc(1.5em + .75rem + 2px);
align-self: center;
`