﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Microsoft.StreamProcessing
{
    using System.Linq;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    internal partial class SessionWindowTemplate : CommonUnaryTemplate
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write(@"// *********************************************************************
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License
// *********************************************************************
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.StreamProcessing;
using Microsoft.StreamProcessing.Aggregates;
using Microsoft.StreamProcessing.Internal;
using Microsoft.StreamProcessing.Internal.Collections;

// TKey: ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write("\r\n// TPayload: ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write("\r\n\r\n[DataContract]\r\ninternal sealed class ");
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTPayloadGenericParameters));
            this.Write(" : UnaryPipe<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(">\r\n{\r\n    private readonly MemoryPool<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(@"> pool;
    private readonly Func<PlanNode, IQueryObject, PlanNode> queryPlanGenerator;

    [SchemaSerialization]
    private readonly long sessionTimeout;
    [SchemaSerialization]
    private readonly long maximumDuration;

    private StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write("> genericOutputBatch;\r\n    [DataMember]\r\n    private ");
            this.Write(this.ToStringHelper.ToStringWithCulture(BatchGeneratedFrom_TKey_TPayload));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTPayloadGenericParameters));
            this.Write(" output;\r\n\r\n    private LinkedList<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write("> orderedKeys = new LinkedList<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(">();\r\n    [DataMember]\r\n    private FastDictionary2<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", long> windowEndTimeDictionary = new FastDictionary2<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", long>();\r\n    [DataMember]\r\n    private FastDictionary2<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", long> lastDataTimeDictionary = new FastDictionary2<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", long>();\r\n    [DataMember]\r\n    private FastDictionary2<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", Queue<ActiveEvent>> stateDictionary = new FastDictionary2<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", Queue<ActiveEvent>>();\r\n\r\n    ");
            this.Write(this.ToStringHelper.ToStringWithCulture(staticCtor));
            this.Write("\r\n\r\n    [Obsolete(\"Used only by serialization. Do not call directly.\")]\r\n    publ" +
                    "ic ");
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            this.Write("() { }\r\n\r\n    public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            this.Write("(\r\n        IStreamable<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write("> stream,\r\n        IStreamObserver<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(@"> observer,
        Func<PlanNode, IQueryObject, PlanNode> queryPlanGenerator,
        long sessionTimeout,
        long maximumDuration)
        : base(stream, observer)
    {
        this.sessionTimeout = sessionTimeout;
        this.maximumDuration = maximumDuration;
        pool = MemoryManager.GetMemoryPool<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(@">(true /*stream.Properties.IsColumnar*/);
        this.queryPlanGenerator = queryPlanGenerator;
        GetOutputBatch();
    }

    private void GetOutputBatch()
    {
        pool.Get(out genericOutputBatch);
        genericOutputBatch.Allocate();
        output = (");
            this.Write(this.ToStringHelper.ToStringWithCulture(BatchGeneratedFrom_TKey_TPayload));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTPayloadGenericParameters));
            this.Write(")genericOutputBatch;\r\n");
 foreach (var f in this.fields.Where(fld => fld.OptimizeString())) {  
            this.Write("\r\n        output.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".Initialize();\r\n");
 } 
            this.Write("   }\r\n\r\n    public override void ProduceQueryPlan(PlanNode previous)\r\n    {\r\n    " +
                    "    Observer.ProduceQueryPlan(queryPlanGenerator(previous, this));\r\n    }\r\n\r\n   " +
                    " private void ReachTime(int pIndex, long timestamp)\r\n    {\r\n        if (pIndex !" +
                    "= -1 && maximumDuration < StreamEvent.InfinitySyncTime)\r\n        {\r\n            " +
                    "if (windowEndTimeDictionary.entries[pIndex].value == StreamEvent.InfinitySyncTim" +
                    "e)\r\n            {\r\n                long mod = timestamp % maximumDuration;\r\n    " +
                    "            windowEndTimeDictionary.entries[pIndex].value = timestamp - mod + ((" +
                    "mod == 0 ? 1 : 2) * maximumDuration);\r\n            }\r\n            else if (windo" +
                    "wEndTimeDictionary.entries[pIndex].value == StreamEvent.MaxSyncTime)\r\n          " +
                    "  {\r\n                windowEndTimeDictionary.entries[pIndex].value = timestamp -" +
                    " (timestamp % maximumDuration) + maximumDuration;\r\n            }\r\n        }\r\n\r\n " +
                    "       var current = orderedKeys.First;\r\n        while (current != null)\r\n      " +
                    "  {\r\n            int cIndex;\r\n            lastDataTimeDictionary.Lookup(current." +
                    "Value, out cIndex);\r\n            var threshhold = lastDataTimeDictionary.entries" +
                    "[cIndex].value == long.MinValue\r\n                ? windowEndTimeDictionary.entri" +
                    "es[cIndex].value\r\n                : Math.Min(lastDataTimeDictionary.entries[cInd" +
                    "ex].value + sessionTimeout, windowEndTimeDictionary.entries[cIndex].value);\r\n   " +
                    "         if (timestamp >= threshhold)\r\n            {\r\n                var queue " +
                    "= stateDictionary.entries[cIndex].value;\r\n                while (queue.Any())\r\n " +
                    "               {\r\n                    var active = queue.Dequeue();\r\n\r\n         " +
                    "           int ind = output.Count++;\r\n                    output.vsync.col[ind] " +
                    "= threshhold;\r\n                    output.vother.col[ind] = active.Sync;\r\n      " +
                    "              output.key.col[ind] = active.Key;\r\n                    //output[in" +
                    "d] = active.Payload;\r\n");
     foreach (var f in this.fields) { 
       if (f.OptimizeString()) { 
            this.Write("\r\n                    output.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".AddString(active.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(");\r\n");
       } else { 
            this.Write("                    output.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".col[ind] = active.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(";\r\n");
       } 
     } 
            this.Write(@"                    output.hash.col[ind] = active.Hash;

                    if (output.Count == Config.DataBatchSize) FlushContents();
                }
                if (timestamp < lastDataTimeDictionary.entries[cIndex].value + sessionTimeout)
                    windowEndTimeDictionary.entries[cIndex].value = StreamEvent.MaxSyncTime;
                else
                {
                    windowEndTimeDictionary.Remove(current.Value);
                    lastDataTimeDictionary.Remove(current.Value);
                    stateDictionary.Remove(current.Value);
                }
                orderedKeys.RemoveFirst();
                current = orderedKeys.First;
            }
            else break;
        }
    }

    private int AllocatePartition(");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(@" pKey)
    {
        windowEndTimeDictionary.Insert(pKey, StreamEvent.InfinitySyncTime);
        lastDataTimeDictionary.Insert(pKey, long.MinValue);
        return stateDictionary.Insert(pKey, new Queue<ActiveEvent>());
    }

    public override unsafe void OnNext(StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write("> genericBatch)\r\n    {\r\n        var batch = genericBatch as ");
            this.Write(this.ToStringHelper.ToStringWithCulture(BatchGeneratedFrom_TKey_TPayload));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTPayloadGenericParameters));
            this.Write(@";
        var count = batch.Count;

        fixed (long* bv = batch.bitvector.col)
        fixed (long* vsync = batch.vsync.col)
        fixed (long* vother = batch.vother.col)
        fixed (int* hash = batch.hash.col)
        {
            for (int i = 0; i < count; i++)
            {
                if ((bv[i >> 6] & (1L << (i & 0x3f))) == 0)
                {
                    if (vsync[i] > vother[i]) // We have an end edge
                    {
                        ReachTime(-1, vsync[i]);
                    }
                    else
                    {
                        int keyIndex;
                        // Check to see if the key is already being tracked
                        if (!lastDataTimeDictionary.Lookup(batch.key.col[i], out keyIndex))
                            keyIndex = AllocatePartition(batch.key.col[i]);
                        ReachTime(keyIndex, vsync[i]);

                        // Check to see if advancing time removed the key
                        if (!lastDataTimeDictionary.Lookup(batch.key.col[i], out keyIndex))
                            keyIndex = AllocatePartition(batch.key.col[i]);

                        if (!stateDictionary.entries[keyIndex].value.Any()) orderedKeys.AddLast(new LinkedListNode<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(@">(batch.key.col[i]));
                        else
                        {
                            var oldThreshhold = Math.Min(lastDataTimeDictionary.entries[keyIndex].value + sessionTimeout, windowEndTimeDictionary.entries[keyIndex].value);
                            var newThreshhold = Math.Min(vsync[i] + sessionTimeout, windowEndTimeDictionary.entries[keyIndex].value);
                            if (newThreshhold > oldThreshhold)
                            {
                                var node = orderedKeys.Find(batch.key.col[i]);
                                orderedKeys.Remove(node);
                                orderedKeys.AddLast(node);
                            }
                        }
                        lastDataTimeDictionary.entries[keyIndex].value = vsync[i];
                        var activeEvent = new ActiveEvent();
                        activeEvent.Populate(batch.key.col[i], batch, i, hash[i], vsync[i]);
                        stateDictionary.entries[keyIndex].value.Enqueue(activeEvent);

                        int ind = output.Count++;
                        output.vsync.col[ind] = vsync[i];
                        output.vother.col[ind] = StreamEvent.InfinitySyncTime;
                        output.key.col[ind] = batch.key.col[i];
");
     foreach (var f in this.fields) { 
       if (f.OptimizeString()) { 
            this.Write("                        output.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".AddString(batch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write("[i]);\r\n");
       } else { 
            this.Write("                        output.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".col[ind] = batch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".col[i];\r\n");
       } 
     } 
            this.Write(@"                        output.hash.col[ind] = hash[i];

                        if (output.Count == Config.DataBatchSize) FlushContents();
                    }
                }
                else if (vother[i] == long.MinValue)
                {
                    ReachTime(-1, vsync[i]);

                    int ind = output.Count++;
                    output.vsync.col[ind] = vsync[i];
                    output.vother.col[ind] = long.MinValue;
                    output.key.col[ind] = default;
                    output[ind] = default;
                    output.hash.col[ind] = hash[i];
                    output.bitvector.col[ind >> 6] |= (1L << (ind & 0x3f));

                    if (output.Count == Config.DataBatchSize) FlushContents();
                }
            }
        }

        batch.Free();
    }

    protected override void UpdatePointers()
    {
        int iter = FastDictionary<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", long>.IteratorStart;\r\n        List<Tuple<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", long>> temp = new List<Tuple<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(@", long>>();
        while (lastDataTimeDictionary.Iterate(ref iter))
            if (stateDictionary.entries[iter].value.Any())
                temp.Add(Tuple.Create(
                    lastDataTimeDictionary.entries[iter].key,
                    Math.Min(lastDataTimeDictionary.entries[iter].value + sessionTimeout, windowEndTimeDictionary.entries[iter].value)));
        foreach (var item in temp.OrderBy(o => o.Item2)) orderedKeys.AddLast(new LinkedListNode<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(@">(item.Item1));
        base.UpdatePointers();
    }

    protected override void FlushContents()
    {
        if (output.Count == 0) return;
        this.Observer.OnNext(output);
        GetOutputBatch();
    }

    protected override void DisposeState()
    {
        output.Free();
        windowEndTimeDictionary.Clear();
        lastDataTimeDictionary.Clear();
        stateDictionary.Clear();
    }

    public override int CurrentlyBufferedOutputCount => output.Count;

    public override int CurrentlyBufferedInputCount
    {
        get
        {
            int count = 0;
            int iter = FastDictionary<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", Queue<ActiveEvent>>.IteratorStart;\r\n            while (stateDictionary.Iterate(" +
                    "ref iter)) count += stateDictionary.entries[iter].value.Count();\r\n            re" +
                    "turn count;\r\n        }\r\n    }\r\n\r\n    [DataContract]\r\n    private struct ActiveEv" +
                    "ent\r\n    {\r\n");
 foreach (var f in this.fields) { 
            this.Write("        [DataMember]\r\n        public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Type.GetCSharpSourceSyntax()));
            this.Write(" ");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(";\r\n");
 } 
            this.Write("        [DataMember]\r\n        public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(" Key;\r\n        [DataMember]\r\n        public int Hash;\r\n        [DataMember]\r\n    " +
                    "    public long Sync;\r\n\r\n        public void Populate(");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(" key, ");
            this.Write(this.ToStringHelper.ToStringWithCulture(BatchGeneratedFrom_TKey_TPayload));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTPayloadGenericParameters));
            this.Write(" batch, int index, int hash, long sync)\r\n        {\r\n            this.Key = key;\r\n" +
                    "            //this.Payload = payload;\r\n");
 foreach (var f in this.fields) { 
            this.Write("            this.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(" = ");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.AccessExpressionForRowValue("batch", "index")));
            this.Write(";\r\n");
 } 
            this.Write("            this.Hash = hash;\r\n            this.Sync = sync;\r\n        }\r\n\r\n      " +
                    "  public override string ToString()\r\n        {\r\n            return \"Key=\'\" + Key" +
                    " + \"\', Payload=\'\"; // + Payload;\r\n        }\r\n    }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
}
