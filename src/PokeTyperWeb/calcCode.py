
for mon in party:
    mon["score"] = 0
    for rtype in mon["types"]:
        if rtype in typeInfo.Resist2x:
            mon["score"] -= 2
        if rtype in typeInfo.Resist4x:
            mon["score"] -= 4
        if rtype in typeInfo.Immune:
            mon["score"] -= 8
        if rtype in typeInfo.WeakTo2x:
            mon["score"] += 2
        if rtype in typeInfo.WeakTo4x:
            mon["score"] += 4
        if rtype in typeInfo.Normal:
            mon["score"] += 1
    for move in mon["moves"]:
        if move.type in WeakTo2x:
            mon["score"] += 2
        if move.type in WeakTo4x:
            mon["score"] += 4
        if move.type in Normal:
            mon["score"] += 1


