# Task2
You have a collection of units (Unit) that described an org chart structure. Create an algorithm to reorganize units collection to Dictionary<int, List<Unit>> in accordance with the following rules:
<li>	Dictionary Key – Id of the unit. All unit ids should be represented in dictionary keys.</li>
<li>	Dictionary Value – collection of all child units for corresponding key (direct and not direct, see example)</li>
<li>	Unit1 is the child of Unit2 if Unit1.ParentUnitId = Unit2.Id or if the Unit1 is the child of child of Unit2.</li>
<br>
<p>Requirements:</p>

<li>  Create test data (collection of units)</li>
<li>	Implement service that use your test data and returns dictionary of child units</li>
<li>	Show test and result data (use console for the output)</li>
<li>	Try to handle as many corner cases as possible (parent unit does not exist, cycle dependency etc.)</li>
