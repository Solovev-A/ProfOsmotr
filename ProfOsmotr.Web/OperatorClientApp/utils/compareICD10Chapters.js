export default function compareICD10Chapters(a, b) {
    const aBlock = a.block;
    const bBlock = b.block;

    if (aBlock > bBlock) return 1;
    if (aBlock < bBlock) return -1;
    return 0;
}